# Deployment Checklist

Use this checklist when deploying CodeMart.Server to a new computer or server.

## Pre-Deployment

- [ ] Docker and Docker Compose are installed on the target machine
- [ ] Git is installed (if cloning from repository)
- [ ] Network ports 8080 (and optionally 5432) are available
- [ ] You have access to Supabase credentials (if using Supabase)

## Step-by-Step Deployment

### 1. Transfer Files

**Option A: Using Git (Recommended)**
```bash
git clone <repository-url>
cd CodeMart.Server
```

**Option B: Copy Files Manually**
- Copy the entire `CodeMart.Server` directory
- Ensure all files are included (especially `Dockerfile`, `docker-compose.yml`, etc.)

### 2. Create Environment File

Create a `.env` file in the project root with the following variables:

```env
# PostgreSQL Password
POSTGRES_PASSWORD=your-secure-password-here

# JWT Configuration
JWT_KEY=your-32-character-minimum-secret-key-here
JWT_ISSUER=CodeMart.Server
JWT_AUDIENCE=CodeMart.Client
JWT_ACCESS_TOKEN_EXPIRATION_MINUTES=60

# Supabase Configuration
SUPABASE_PROJECT_URL=https://your-project.supabase.co
SUPABASE_API_KEY=your-supabase-api-key
```

**Important:**
- Generate a strong `JWT_KEY` (at least 32 characters)
- Use a secure `POSTGRES_PASSWORD`
- Never commit the `.env` file to version control

### 3. Verify Docker Setup

```bash
# Check Docker is running
docker --version
docker-compose --version

# Verify docker-compose.yml is valid
docker-compose config
```

### 4. Start Services

```bash
# Build and start all services
docker-compose up -d

# Check that containers are running
docker-compose ps
```

Expected output should show:
- `codemart-postgres` - Status: Up
- `codemart-api` - Status: Up

### 5. Run Database Migrations

```bash
# Windows
.\docker-migrate.ps1

# Linux/macOS
./docker-migrate.sh

# Or manually
docker-compose exec api dotnet ef database update
```

### 6. Verify Deployment

```bash
# Check API logs
docker-compose logs api

# Test API endpoint (if health check exists)
curl http://localhost:8080/api/health

# Or open in browser
# http://localhost:8080/swagger (Development mode)
```

### 7. Configure Firewall (If Needed)

If deploying on a server accessible from the internet:

```bash
# Linux (UFW example)
sudo ufw allow 8080/tcp

# Or configure your cloud provider's firewall rules
```

## Post-Deployment

- [ ] API is accessible at `http://localhost:8080`
- [ ] Database migrations completed successfully
- [ ] No errors in container logs
- [ ] Environment variables are set correctly
- [ ] Services restart automatically (restart: unless-stopped)

## Troubleshooting

### Containers Won't Start

1. Check logs:
   ```bash
   docker-compose logs
   ```

2. Verify environment variables:
   ```bash
   docker-compose config
   ```

3. Rebuild containers:
   ```bash
   docker-compose down
   docker-compose build --no-cache
   docker-compose up -d
   ```

### Database Connection Issues

1. Ensure PostgreSQL container is healthy:
   ```bash
   docker-compose ps postgres
   ```

2. Check database logs:
   ```bash
   docker-compose logs postgres
   ```

3. Test connection manually:
   ```bash
   docker-compose exec postgres psql -U postgres -d codemart
   ```

### Port Conflicts

If port 8080 is already in use, edit `docker-compose.yml`:

```yaml
ports:
  - "8081:8080"  # Change 8081 to any available port
```

Then restart:
```bash
docker-compose down
docker-compose up -d
```

## Production Considerations

- [ ] Use strong, unique passwords for all services
- [ ] Enable HTTPS (configure in `Program.cs` or use reverse proxy)
- [ ] Set up regular database backups
- [ ] Configure log rotation
- [ ] Set up monitoring/alerting
- [ ] Review and restrict exposed ports
- [ ] Use secrets management (Docker secrets, AWS Secrets Manager, etc.)
- [ ] Set up CI/CD pipeline for automated deployments

## Backup and Recovery

### Backup Database

```bash
docker-compose exec postgres pg_dump -U postgres codemart > backup_$(date +%Y%m%d_%H%M%S).sql
```

### Restore Database

```bash
docker-compose exec -T postgres psql -U postgres codemart < backup.sql
```

## Updating the Application

1. Pull latest changes (if using Git):
   ```bash
   git pull
   ```

2. Rebuild and restart:
   ```bash
   docker-compose down
   docker-compose build --no-cache api
   docker-compose up -d
   ```

3. Run migrations if schema changed:
   ```bash
   docker-compose exec api dotnet ef database update
   ```

## Security Checklist

- [ ] `.env` file is not committed to version control
- [ ] Strong passwords are used for all services
- [ ] JWT key is at least 32 characters and randomly generated
- [ ] Database is not exposed to the internet (if using docker-compose)
- [ ] Firewall rules are properly configured
- [ ] HTTPS is enabled for production
- [ ] Regular security updates are applied

