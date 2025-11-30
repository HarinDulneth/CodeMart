using CodeMart.Server.Data;
using CodeMart.Server.Interfaces;
using CodeMart.Server.Services;
using CodeMart.Server.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;
using Supabase;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Get Supabase configuration from environment variables
var supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_PROJECT_URL")
    ?? throw new InvalidOperationException("SUPABASE_PROJECT_URL environment variable is required");
var supabaseApiKey = Environment.GetEnvironmentVariable("SUPABASE_API_KEY")
    ?? throw new InvalidOperationException("SUPABASE_API_KEY environment variable is required");

var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true,
};

// Add Supabase Client
builder.Services.AddSingleton(provider => new Supabase.Client(supabaseUrl, supabaseApiKey, options));

// Add Entity Framework Core with PostgreSQL
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
    ?? throw new InvalidOperationException("DB_CONNECTION_STRING environment variable is required");

IServiceCollection serviceCollection = builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configure JWT Options from environment variables
var jwtOptions = new JwtOptions
{
    Key = Environment.GetEnvironmentVariable("JWT_KEY") 
        ?? throw new InvalidOperationException("JWT_KEY environment variable is required"),
    Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "CodeMart.Server",
    Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "CodeMart.Client",
    AccessTokenExpirationMinutes = int.TryParse(
        Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRATION_MINUTES"), 
        out int expiration) ? expiration : 60
};

builder.Services.Configure<JwtOptions>(options =>
{
    options.Key = jwtOptions.Key;
    options.Issuer = jwtOptions.Issuer;
    options.Audience = jwtOptions.Audience;
    options.AccessTokenExpirationMinutes = jwtOptions.AccessTokenExpirationMinutes;
});

// Register Services
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

// Configure JWT Authentication
if (!string.IsNullOrWhiteSpace(jwtOptions.Key))
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ValidateIssuer = !string.IsNullOrWhiteSpace(jwtOptions.Issuer),
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = !string.IsNullOrWhiteSpace(jwtOptions.Audience),
            ValidAudience = jwtOptions.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
}

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Serialize enums as strings instead of integers for better API usability
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
