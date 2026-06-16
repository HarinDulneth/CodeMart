<div align="center">

<img src="https://img.shields.io/badge/CodeMart-Marketplace-6366f1?style=for-the-badge&logo=shopify&logoColor=white" alt="CodeMart" />

# CodeMart

**A modern, full-stack marketplace for buying and selling software projects.**

[![.NET](https://img.shields.io/badge/.NET_8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![React](https://img.shields.io/badge/React_18-61DAFB?style=flat-square&logo=react&logoColor=black)](https://react.dev/)
[![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=flat-square&logo=typescript&logoColor=white)](https://www.typescriptlang.org/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Supabase](https://img.shields.io/badge/Supabase-3ECF8E?style=flat-square&logo=supabase&logoColor=white)](https://supabase.com/)
[![Stripe](https://img.shields.io/badge/Stripe-635BFF?style=flat-square&logo=stripe&logoColor=white)](https://stripe.com/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](LICENSE)

</div>

---

## Overview

**CodeMart** is a full-stack software marketplace that connects developers who want to sell their projects with buyers looking for ready-made solutions. It features a highly interactive React frontend with 3D elements and smooth animations, backed by a robust ASP.NET Core REST API with JWT authentication, Stripe payment integration, and a PostgreSQL database hosted on Supabase.

---

## ✨ Features

| Feature | Description |
|---|---|
| 🛍️ **Project Marketplace** | Browse, filter, search, and discover software projects for sale |
| 🔐 **Secure Authentication** | JWT-based auth with BCrypt password hashing and role-based access control |
| 💳 **Stripe Payments** | End-to-end payment processing with Stripe Checkout and webhooks |
| ⭐ **Reviews & Ratings** | Buyers can leave verified feedback on purchased projects |
| 🛒 **Cart & Checkout** | Full shopping cart with order management and transaction history |
| 👤 **User Profiles** | Seller profiles, portfolio listings, and purchase history |
| 🎨 **Rich UI/UX** | 3D visuals, smooth scroll, micro-animations, and fully responsive design |
| 🐳 **Docker Ready** | Containerized backend for seamless deployment to any cloud provider |

---

## 🏗️ Architecture

```
CodeMart/
├── CodeMart-Frontend/          # React 18 + TypeScript + Vite (SPA)
│   └── src/
│       ├── components/         # Reusable UI components
│       ├── pages/              # Route-level page components
│       ├── hooks/              # Custom React hooks
│       └── lib/                # API clients, utilities
│
└── CodeMart.Server/            # ASP.NET Core 8 REST API
    ├── Controllers/            # API endpoints (Auth, Projects, Orders, Reviews, ...)
    ├── Models/                 # EF Core entity models
    ├── DTOs/                   # Data Transfer Objects
    ├── Services/               # Business logic layer
    ├── Interfaces/             # Service contracts
    ├── Data/                   # DbContext & migrations
    └── Utils/                  # Helpers and middleware
```

---

## 💻 Tech Stack

### Frontend — `CodeMart-Frontend/`

| Category | Technology |
|---|---|
| **Framework** | React 18, TypeScript, Vite |
| **Routing** | React Router DOM |
| **Styling** | Tailwind CSS |
| **UI Components** | Radix UI, Lucide React, Tabler Icons |
| **Animations & 3D** | Framer Motion, GSAP, Three.js |
| **Smooth Scroll** | Lenis |
| **Payments** | Stripe Elements |
| **Auth & DB** | Supabase Client, Google OAuth |

### Backend — `CodeMart.Server/`

| Category | Technology |
|---|---|
| **Framework** | ASP.NET Core 8, C# 12 |
| **Database** | PostgreSQL (via Supabase) |
| **ORM** | Entity Framework Core 9 |
| **Authentication** | JWT (JSON Web Tokens), BCrypt |
| **Payments** | Stripe.net SDK |
| **API Docs** | Swagger / OpenAPI |
| **Containerization** | Docker, Docker Compose |

---

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed and accounts set up:

- [Node.js](https://nodejs.org/) v18 or higher
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- A [Supabase](https://supabase.com/) account and project (PostgreSQL database)
- A [Stripe](https://stripe.com/) account (for payment processing)
- [Docker](https://www.docker.com/) *(optional, for containerized backend)*

### 1. Clone the Repository

```bash
git clone https://github.com/HarinDulneth/CodeMart.git
cd CodeMart
```

---

### 2. Backend Setup (`CodeMart.Server`)

```bash
cd CodeMart.Server
```

**a. Configure environment variables**

Copy the example environment file and fill in your credentials:

```bash
cp .env.example .env
```

| Variable | Description |
|---|---|
| `ConnectionStrings__DefaultConnection` | Your Supabase PostgreSQL connection string |
| `JwtSettings__Secret` | A long, random secret key for JWT signing |
| `Stripe__SecretKey` | Your Stripe secret key (`sk_...`) |
| `Stripe__WebhookSecret` | Your Stripe webhook signing secret (`whsec_...`) |

**b. Apply database migrations**

```bash
dotnet ef database update
```

**c. Run the API server**

```bash
dotnet run
```

The API will start on **`http://localhost:8080`**.  
Swagger UI is available at **`http://localhost:8080/swagger`**.

> 📖 See [DOCKER_SETUP.md](DOCKER_SETUP.md) for Docker-based deployment instructions.

---

### 3. Frontend Setup (`CodeMart-Frontend`)

Open a new terminal from the project root:

```bash
cd CodeMart-Frontend
```

**a. Install dependencies**

```bash
npm install
```

**b. Configure environment variables**

Create a `.env` file in the `CodeMart-Frontend/` directory:

```env
VITE_API_BASE_URL=http://localhost:8080
VITE_STRIPE_PUBLISHABLE_KEY=pk_test_...
VITE_SUPABASE_URL=https://your-project.supabase.co
VITE_SUPABASE_ANON_KEY=your-anon-key
```

**c. Start the development server**

```bash
npm run dev
```

The frontend will be available at **`http://localhost:5173`**.

---

## 🐳 Docker Deployment

The backend is fully containerized and can be deployed with Docker Compose:

```bash
# Production
docker-compose up -d

# Development (with hot-reload)
docker-compose -f docker-compose.dev.yml up
```

> 📖 See [DOCKER_SETUP.md](DOCKER_SETUP.md) and [SUPABASE_SETUP.md](SUPABASE_SETUP.md) for detailed setup guides.

---

## 📦 Deployment

| Target | Details |
|---|---|
| **Backend** | Docker image deployable to AWS ECS, Azure App Service, DigitalOcean App Platform, Railway, Render |
| **Frontend** | Static Vite build (`npm run build`) deployable to Vercel, Netlify, Cloudflare Pages, or containerized with the included `Dockerfile` + `nginx.conf` |

---

## 📡 API Overview

The backend exposes a RESTful API documented via Swagger. Key endpoint groups:

| Controller | Base Route | Description |
|---|---|---|
| `AuthController` | `/api/auth` | Registration, login, token refresh |
| `ProjectController` | `/api/projects` | CRUD for marketplace listings |
| `UserController` | `/api/users` | User profiles and account management |
| `OrderController` | `/api/orders` | Order placement and history |
| `TransactionController` | `/api/transactions` | Stripe payment intents & webhooks |
| `ReviewController` | `/api/reviews` | Buyer reviews and ratings |

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch: `git checkout -b feature/your-feature-name`
3. **Commit** your changes with a descriptive message: `git commit -m 'feat: add amazing feature'`
4. **Push** to your branch: `git push origin feature/your-feature-name`
5. **Open a Pull Request** against the `main` branch

Please ensure your code follows the existing conventions and that any new API endpoints are covered by the Swagger annotations.

---

## 📄 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

<div align="center">

Made with ❤️ by [HarinDulneth](https://github.com/HarinDulneth)

</div>