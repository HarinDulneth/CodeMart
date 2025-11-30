# Docker Setup Guide for CodeMart.Server

This guide explains how to run the CodeMart.Server application using Docker.

## Prerequisites

- Docker Desktop installed (or Docker Engine + Docker Compose)
- Docker version 20.10 or higher
- Docker Compose version 2.0 or higher

## Quick Start

### Option 1: Using Docker Compose (Recommended)

This will start both the API and PostgreSQL database:

```bash
# Build and start all services
docker-compose up -d

# View logs
docker-compose logs -f api

# Stop all services
docker-compose down

# Stop and remove volumes (WARNING: This deletes database data)
docker-compose down -v
```

### Option 2: Using Docker Only (API Only)

If you already have a PostgreSQL database running:

```bash
# Build the image
docker build -t codemart-server .

# Run the container
docker run -d \
  --name codemart-api \
  -p 8080:8080 \
  -e DB_CONNECTION_STRING="Host=your-db-host;Port=5432;Database=codemart;Username=postgres;Password=your-password" \
  -e Jwt__Key="YourSuperSecretKeyThatShouldBeAtLeast32CharactersLong" \
  -e Jwt__Issuer="CodeMart.Server" \
  -e Jwt__Audience="CodeMart.Client" \
  codemart-server
```

## Environment Variables

Create a `.env` file in the root directory (optional, for docker-compose):

```env
POSTGRES_PASSWORD=your-secure-password
JWT_KEY=YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForHS256Algorithm
JWT_ISSUER=CodeMart.Server
JWT_AUDIENCE=CodeMart.Client
JWT_EXPIRATION=60
SUPABASE_PROJECT_URL=https://your-project.supabase.co
SUPABASE_API_KEY=your-supabase-api-key
```

## Configuration

### Database Connection

The application will automatically run migrations on startup in Development mode. For Production, you may want to run migrations manually:

```bash
# Run migrations manually
docker-compose exec api dotnet ef database update
```

### Ports

- **API**: `http://localhost:8080` (HTTP)
- **API**: `https://localhost:8081` (HTTPS - if configured)
- **PostgreSQL**: `localhost:5432`

### Changing Ports

Edit `docker-compose.yml` to change ports:

```yaml
ports:
  - "5000:8080" # Change 5000 to your desired port
```

## Development

For development with hot-reload, use the override file:

```bash
# This uses docker-compose.override.yml automatically
docker-compose up
```

## Production Deployment

### 1. Build for Production

```bash
docker build -t codemart-server:latest .
```

### 2. Run with Production Settings

```bash
docker run -d \
  --name codemart-api \
  -p 8080:8080 \
  --env-file .env.production \
  --restart unless-stopped \
  codemart-server:latest
```

### 3. Using Docker Compose for Production

```bash
# Use production compose file
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d
```

## Health Checks

Check if the API is running:

```bash
# Check container status
docker ps

# Check API health
curl http://localhost:8080/api/health

# View API logs
docker-compose logs -f api
```

## Troubleshooting

### Database Connection Issues

1. Ensure PostgreSQL container is healthy:

   ```bash
   docker-compose ps
   ```

2. Check database logs:

   ```bash
   docker-compose logs postgres
   ```

3. Verify connection string in environment variables

### Migration Issues

If migrations fail, run them manually:

```bash
docker-compose exec api dotnet ef database update
```

### Port Already in Use

Change the port mapping in `docker-compose.yml`:

```yaml
ports:
  - "8081:8080" # Use different host port
```

### View Logs

```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f api
docker-compose logs -f postgres
```

## Clean Up

```bash
# Stop containers
docker-compose down

# Remove containers and volumes
docker-compose down -v

# Remove images
docker rmi codemart-server
```

## Security Notes

1. **Never commit `.env` files** with real credentials
2. **Use strong JWT keys** in production (at least 32 characters)
3. **Change default PostgreSQL password** in production
4. **Use HTTPS** in production (configure in `Program.cs`)
5. **Limit exposed ports** to only what's necessary

## Additional Resources

- [Docker Documentation](https://docs.docker.com/)
- [Docker Compose Documentation](https://docs.docker.com/compose/)
- [ASP.NET Core Docker Documentation](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/)
