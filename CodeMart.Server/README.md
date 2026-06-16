<div align="center">

<img src="https://img.shields.io/badge/CodeMart-Backend_API-6366f1?style=for-the-badge&logo=dotnet&logoColor=white" alt="CodeMart Backend" />

# CodeMart.Server

**The RESTful backend API powering the CodeMart software marketplace.**

[![.NET](https://img.shields.io/badge/.NET_8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23_12-239120?style=flat-square&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![EF Core](https://img.shields.io/badge/EF_Core_9-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Supabase](https://img.shields.io/badge/Supabase-3ECF8E?style=flat-square&logo=supabase&logoColor=white)](https://supabase.com/)
[![Stripe](https://img.shields.io/badge/Stripe-635BFF?style=flat-square&logo=stripe&logoColor=white)](https://stripe.com/)
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=flat-square&logo=swagger&logoColor=black)](https://swagger.io/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](../LICENSE)

</div>

---

## Overview

**CodeMart.Server** is an ASP.NET Core 8 Web API that handles all business logic, data persistence, and secure communications for the CodeMart platform. It exposes a fully documented RESTful API for user authentication, project listings, shopping cart operations, Stripe payment processing, and buyer reviews.

---

## ✨ Features

| Feature | Description |
|---|---|
| 🔐 **JWT Authentication** | Secure token-based auth with BCrypt password hashing and role-based access control (RBAC) |
| 🌐 **Google OAuth** | Sign-in with Google via `Google.Apis.Auth` token verification |
| 🛍️ **Project Marketplace** | Full CRUD for software project listings with filtering and category support |
| 🛒 **Cart & Orders** | Shopping cart management and complete order lifecycle |
| 💳 **Stripe Payments** | Payment intent creation, confirmation, and webhook handling |
| ⭐ **Reviews & Ratings** | Verified buyer reviews tied to completed purchases |
| 📊 **Transaction History** | Transparent tracking of payment statuses per user |
| 📄 **Swagger / OpenAPI** | Interactive API documentation with JWT auth support |
| 🐳 **Docker Ready** | Multi-stage Docker image with Docker Compose for one-command setup |

---

## 💻 Tech Stack

| Category | Technology | Version |
|---|---|---|
| **Framework** | ASP.NET Core Web API | .NET 8.0 |
| **Language** | C# | 12 |
| **ORM** | Entity Framework Core | 9.0.11 |
| **Database Driver** | Npgsql (PostgreSQL) | 10.0.0 |
| **Database Host** | Supabase (managed PostgreSQL) | — |
| **Authentication** | Microsoft.AspNetCore.Authentication.JwtBearer | 8.0.20 |
| **Password Hashing** | BCrypt.Net-Next | 4.0.3 |
| **Google Auth** | Google.Apis.Auth | 1.73.0 |
| **Supabase Client** | Supabase | 1.1.1 |
| **Payments** | Stripe.net | 50.1.0 |
| **API Docs** | Swashbuckle.AspNetCore (Swagger) | 6.6.2 |
| **Env Config** | DotNetEnv | 3.1.1 |
| **Containerisation** | Docker & Docker Compose | — |

---

## 🏗️ Architecture & Project Structure

The server follows a clean, layered service-oriented architecture with full dependency injection.

```
CodeMart.Server/
├── Controllers/              # HTTP request handlers & routing
│   ├── AuthController.cs     # Registration, login, token refresh, Google OAuth
│   ├── ProjectController.cs  # Project listings CRUD & search
│   ├── UserController.cs     # User profile & account management
│   ├── OrderController.cs    # Order placement & history
│   ├── TransactionController.cs  # Stripe payment intents & webhooks
│   └── ReviewController.cs   # Buyer reviews & ratings
│
├── Services/                 # Core business logic layer
│   ├── AuthenticateService.cs
│   ├── JwtTokenService.cs
│   ├── ProjectService.cs
│   ├── UserService.cs
│   ├── OrderService.cs
│   ├── TransactionService.cs
│   └── ReviewService.cs
│
├── Models/                   # EF Core domain entities (DB tables)
│   ├── User.cs
│   ├── Project.cs
│   ├── Cart.cs
│   ├── Order.cs
│   ├── Transaction.cs
│   ├── TransactionStatus.cs
│   ├── Review.cs
│   ├── Category.cs
│   ├── PaymentMethod.cs
│   └── Permissions.cs
│
├── DTOs/                     # Request / Response data contracts
├── Interfaces/               # Service abstractions for DI
├── Data/                     # EF Core DbContext & configurations
├── Options/                  # Strongly-typed configuration models
├── Utils/                    # Helper classes & extension methods
│
├── Program.cs                # App startup, DI registration, middleware pipeline
├── Dockerfile                # Multi-stage Docker image
├── docker-compose.yml        # Production compose file
├── docker-compose.yml (dev)  # (in root) Dev compose with hot-reload
├── docker-migrate.ps1        # Windows migration helper script
├── docker-migrate.sh         # Linux/macOS migration helper script
└── appsettings.json          # Base app configuration
```

---

## 📡 API Reference

Full interactive documentation is available via Swagger UI at **`http://localhost:8080/swagger`** when running in Development mode.

| Controller | Base Route | Responsibility |
|---|---|---|
| `AuthController` | `/api/auth` | Register, login, JWT refresh, Google OAuth |
| `ProjectController` | `/api/projects` | List, create, update, delete, search projects |
| `UserController` | `/api/users` | Profiles, avatar, account settings, seller listings |
| `OrderController` | `/api/orders` | Place orders, view purchase history |
| `TransactionController` | `/api/transactions` | Stripe payment intents, webhook processing |
| `ReviewController` | `/api/reviews` | Submit and retrieve project reviews |

> **Authenticated routes** require a `Bearer <token>` header. Use the Swagger UI **Authorize** button to set your JWT for in-browser testing.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Git](https://git-scm.com/)
- A [Supabase](https://supabase.com/) project (PostgreSQL database)
- A [Stripe](https://stripe.com/) account (for payment processing)
- [Docker Desktop](https://www.docker.com/) *(optional — for containerised setup)*

---

### 1. Clone & Navigate

```bash
git clone https://github.com/HarinDulneth/CodeMart.git
cd CodeMart/CodeMart.Server
```

---

### 2. Configure Environment Variables

Create a `.env` file in the `CodeMart.Server/` directory:

```env
# PostgreSQL — Supabase connection string
DB_CONNECTION_STRING=Host=db.<project-ref>.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=<password>;SSL Mode=Require

# JWT signing secret (minimum 32 characters)
JWT_KEY=your-super-long-random-jwt-secret-key

# Supabase
SUPABASE_PROJECT_URL=https://<project-ref>.supabase.co
SUPABASE_API_KEY=your-supabase-service-role-key

# Stripe
STRIPE_SECRET_KEY=sk_test_...
STRIPE_WEBHOOK_SECRET=whsec_...
```

> **Note:** Never commit `.env` to version control. It is listed in `.gitignore` by default.  
> See [ENV_SETUP.md](ENV_SETUP.md) for a detailed breakdown of every variable.

---

### 3. Restore & Run

**Option A — .NET CLI (recommended for development)**

```bash
dotnet restore
dotnet run
```

**Option B — Docker Compose**

```bash
docker-compose up -d
docker-compose logs -f api
```

The API will be available at **`http://localhost:8080`**  
Swagger UI: **`http://localhost:8080/swagger`**

---

### 4. Apply Database Migrations

Sync the schema to your Supabase PostgreSQL instance:

```bash
# .NET CLI
dotnet ef database update

# Docker (Windows)
.\docker-migrate.ps1

# Docker (Linux/macOS)
./docker-migrate.sh

# Docker Compose exec
docker-compose exec api dotnet ef database update
```

---

## 🐳 Docker Deployment

The backend ships with a multi-stage `Dockerfile` optimised for production:

```bash
# Build the image
docker build -t codemart-server:latest .

# Run standalone
docker run -d -p 8080:8080 --env-file .env codemart-server:latest
```

Or use Docker Compose for the full stack:

```bash
# Production
docker-compose up -d

# View logs
docker-compose logs -f api
```

> 📖 See [DOCKER.md](DOCKER.md) for advanced networking, volume, and reverse-proxy configurations.

---

## 🗄️ Database

The project uses **Supabase** as a fully managed PostgreSQL host with Entity Framework Core 9 for schema migrations and queries.

**Domain models:**

| Entity | Description |
|---|---|
| `User` | Platform accounts (buyers & sellers) |
| `Project` | Software project listings |
| `Category` | Project categories |
| `Cart` | Shopping cart items |
| `Order` | Completed purchase records |
| `Transaction` | Stripe payment records & statuses |
| `TransactionStatus` | Payment status enum |
| `Review` | Buyer ratings & feedback |
| `PaymentMethod` | Supported payment method types |
| `Permissions` | Role-based permission flags |

You can inspect and manage data directly in the [Supabase Studio dashboard](https://app.supabase.com/).

---

## 📚 Additional Documentation

| Document | Description |
|---|---|
| [DOCKER.md](DOCKER.md) | Docker image build, Compose networking, and deployment |
| [ENV_SETUP.md](ENV_SETUP.md) | Detailed environment variable reference |
| [CHANGELOG.md](CHANGELOG.md) | Version history and release notes |
| [../DOCKER_SETUP.md](../DOCKER_SETUP.md) | Root-level full-stack Docker Compose guide |
| [../SUPABASE_SETUP.md](../SUPABASE_SETUP.md) | Supabase project and database setup |

---

## 🤝 Contributing

1. **Fork** the repository
2. **Create** a feature branch: `git checkout -b feature/your-feature-name`
3. **Commit** your changes using conventional commits: `git commit -m 'feat: add amazing endpoint'`
4. **Push** to your branch: `git push origin feature/your-feature-name`
5. **Open a Pull Request** against the `main` branch

Please ensure:
- The project builds without errors (`dotnet build`)
- All new endpoints have Swagger annotations (`[ProducesResponseType]`)
- New services are registered via dependency injection in `Program.cs`

---

## 📄 License

This project is licensed under the **MIT License** — see the [LICENSE](../LICENSE) file for details.

---

<div align="center">

Part of the [CodeMart](https://github.com/HarinDulneth/CodeMart) monorepo · Made with ❤️ by [HarinDulneth](https://github.com/HarinDulneth)

</div>
