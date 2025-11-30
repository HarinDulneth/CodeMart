# CodeMart.Server

A .NET 8.0 ASP.NET Core Web API server for the CodeMart platform.

## 🚀 Quick Start (For New Computer)

### Prerequisites

- **Docker Desktop** (or Docker Engine + Docker Compose)
  - Windows: Download from [docker.com](https://www.docker.com/products/docker-desktop)
  - Linux: Install Docker Engine and Docker Compose
  - macOS: Download from [docker.com](https://www.docker.com/products/docker-desktop)
- **Git** (to clone the repository)

### Step 1: Clone the Repository

```bash
git clone <your-repository-url>
cd CodeMart.Server
```

### Step 2: Set Up Environment Variables

1. Copy the example environment file:

   ```bash
   # On Windows (PowerShell)
   Copy-Item .env.example .env

   # On Linux/macOS
   cp .env.example .env
   ```

2. Edit `.env` file and fill in your values:
   - `POSTGRES_PASSWORD`: A strong password for PostgreSQL
   - `JWT_KEY`: A secret key (at least 32 characters) for JWT token signing
   - `SUPABASE_PROJECT_URL`: Your Supabase project URL
   - `SUPABASE_API_KEY`: Your Supabase API key

### Step 3: Run with Docker Compose

```bash
# Build and start all services (database + API)
docker-compose up -d

# View logs to ensure everything started correctly
docker-compose logs -f api
```

The API will be available at:

- **HTTP**: `http://localhost:8080`
- **Swagger UI** (Development): `http://localhost:8080/swagger`

### Step 4: Run Database Migrations

Migrations run automatically in Development mode. For Production, run them manually:

```bash
# Using the provided script (Windows)
.\docker-migrate.ps1

# Using the provided script (Linux/macOS)
./docker-migrate.sh

# Or manually
docker-compose exec api dotnet ef database update
```

## 📋 What Gets Started

When you run `docker-compose up -d`, it starts:

1. **PostgreSQL Database** (port 5432)

   - Container: `codemart-postgres`
   - Database: `codemart`
   - Data persists in Docker volume: `postgres_data`

2. **ASP.NET Core API** (port 8080)
   - Container: `codemart-api`
   - Automatically connects to PostgreSQL
   - Runs database migrations on startup (Development mode)

## 🔧 Common Commands

```bash
# Start services
docker-compose up -d

# Stop services
docker-compose down

# View logs
docker-compose logs -f api          # API logs
docker-compose logs -f postgres     # Database logs
docker-compose logs -f              # All logs

# Restart a service
docker-compose restart api

# Check service status
docker-compose ps

# Stop and remove everything (including database data)
docker-compose down -v
```

## 🛠️ Development Mode

For development with hot-reload, the `docker-compose.override.yml` file is automatically used:

```bash
docker-compose up
```

This will:

- Mount your source code as a volume
- Run in Development mode
- Expose ports 5000/5001 instead of 8080/8081

## 📦 Production Deployment

### Option 1: Using Docker Compose (Recommended)

1. Ensure `.env` file has production values
2. Start services:
   ```bash
   docker-compose up -d
   ```
3. Run migrations:
   ```bash
   docker-compose exec api dotnet ef database update
   ```

### Option 2: Build and Run Docker Image Manually

```bash
# Build the image
docker build -t codemart-server:latest .

# Run the container
docker run -d \
  --name codemart-api \
  -p 8080:8080 \
  --env-file .env \
  --restart unless-stopped \
  codemart-server:latest
```

## 🔐 Environment Variables

See `.env.example` for all required environment variables.

**Required:**

- `DB_CONNECTION_STRING` - PostgreSQL connection string (auto-set in docker-compose)
- `JWT_KEY` - Secret key for JWT tokens (min 32 characters)
- `SUPABASE_PROJECT_URL` - Your Supabase project URL
- `SUPABASE_API_KEY` - Your Supabase API key

**Optional (with defaults):**

- `JWT_ISSUER` - Default: "CodeMart.Server"
- `JWT_AUDIENCE` - Default: "CodeMart.Client"
- `JWT_ACCESS_TOKEN_EXPIRATION_MINUTES` - Default: 60

## 🗄️ Database Management

### Access PostgreSQL

```bash
# Connect to PostgreSQL container
docker-compose exec postgres psql -U postgres -d codemart

# Or from host machine (if you have psql installed)
psql -h localhost -p 5432 -U postgres -d codemart
```

### Backup Database

```bash
docker-compose exec postgres pg_dump -U postgres codemart > backup.sql
```

### Restore Database

```bash
docker-compose exec -T postgres psql -U postgres codemart < backup.sql
```

## 🐛 Troubleshooting

### Port Already in Use

If port 8080 is already in use, edit `docker-compose.yml`:

```yaml
ports:
  - "8081:8080" # Change 8081 to any available port
```

### Database Connection Issues

1. Check if PostgreSQL is running:

   ```bash
   docker-compose ps
   ```

2. Check database logs:

   ```bash
   docker-compose logs postgres
   ```

3. Verify connection string in `.env` file

### Migration Issues

If migrations fail on startup, run them manually:

```bash
docker-compose exec api dotnet ef database update
```

### Container Won't Start

1. Check logs:

   ```bash
   docker-compose logs api
   ```

2. Verify all environment variables are set:

   ```bash
   docker-compose config
   ```

3. Rebuild the image:
   ```bash
   docker-compose build --no-cache api
   docker-compose up -d
   ```

## 📚 Additional Documentation

- [DOCKER.md](DOCKER.md) - Detailed Docker setup guide
- [ENV_SETUP.md](ENV_SETUP.md) - Environment variables documentation

## 🔒 Security Notes

1. **Never commit `.env` files** - They contain sensitive credentials
2. **Use strong passwords** in production
3. **Change default JWT keys** - Generate a secure random key
4. **Use HTTPS** in production environments
5. **Limit exposed ports** to only what's necessary

## 📝 API Documentation

Once the server is running, access:

- **Swagger UI**: `http://localhost:8080/swagger` (Development mode only)
- **API Base URL**: `http://localhost:8080/api`

## 🤝 Contributing

1. Clone the repository
2. Create a feature branch
3. Make your changes
4. Test with Docker Compose
5. Submit a pull request

## 📄 License

[Add your license information here]
