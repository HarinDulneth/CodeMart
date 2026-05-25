# 🛒 CodeMart

**CodeMart** is a modern, full-stack marketplace platform designed for buying and selling software projects. This repository contains both the client-side frontend application and the backend API server.

## 🌟 Key Features

- **Project Marketplace**: Browse, filter, search, and discover software projects for sale.
- **Rich User Interface**: Highly interactive and responsive UI built with modern React, complete with 3D elements, smooth scroll, and beautiful animations.
- **Secure Authentication**: Robust JWT-based authentication with Supabase and BCrypt, featuring role-based access controls.
- **Cart & Checkout**: Seamless shopping cart experience with secure payment processing powered by **Stripe**.
- **Reviews & Ratings**: Integrated system for buyers to leave feedback on their purchased projects.
- **Full-Stack Integration**: Complete separation of concerns with a standalone React frontend and a robust .NET Core REST API backend.

## 🏗️ Repository Structure

This repository is structured as a monorepo containing two main projects:

- **`CodeMart-Frontend/`**: The client-side application.
- **`CodeMart.Server/`**: The backend RESTful API server.

---

## 💻 Tech Stack

### Frontend (`CodeMart-Frontend`)
- **Core**: React 18, TypeScript, Vite
- **Routing**: React Router DOM
- **Styling**: Tailwind CSS
- **UI Components**: Radix UI, Lucide React, Tabler Icons
- **Animations & 3D**: Framer Motion, GSAP, Three.js, Lenis (Smooth Scroll)
- **Integrations**: Stripe Elements, Supabase Client, Google OAuth

### Backend (`CodeMart.Server`)
- **Core**: .NET 8.0 (C# 12), ASP.NET Core Web API
- **Database**: PostgreSQL (hosted on [Supabase](https://supabase.com/))
- **ORM**: Entity Framework Core 9
- **Authentication**: JWT (JSON Web Tokens)
- **Integrations**: Stripe.net SDK

---

## 🚀 Getting Started

To run the entire CodeMart platform locally, you will need to set up both the backend and frontend separately. 

### Prerequisites
- [Node.js](https://nodejs.org/) (v18+)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- Supabase Account (for PostgreSQL database)
- Stripe Account (for payment gateway)

### 1. Setting up the Backend

Navigate to the backend directory:
```bash
cd CodeMart.Server
```

1. **Environment Variables**: Copy `.env.example` to `.env` and fill in your Supabase connection strings, JWT secrets, and Stripe Secret Key.
2. **Run Migrations**: 
   ```bash
   dotnet ef database update
   ```
3. **Start the API**: 
   ```bash
   dotnet run
   ```
   *The API will typically start on `http://localhost:8080` (or as configured). You can access the Swagger UI at `http://localhost:8080/swagger`.*

*(For Docker setup instructions, see the `CodeMart.Server/README.md`)*

### 2. Setting up the Frontend

Open a new terminal and navigate to the frontend directory:
```bash
cd CodeMart-Frontend
```

1. **Install Dependencies**:
   ```bash
   npm install
   ```
2. **Environment Variables**: Copy `.env.example` to `.env` (if applicable) and provide the necessary keys (like `VITE_API_BASE_URL`, your Stripe Publishable Key, and Supabase URL/Anon Key).
3. **Start the Development Server**:
   ```bash
   npm run dev
   ```
   *The frontend will typically start on `http://localhost:5173`.*

---

## 📦 Deployment

- **Backend**: Containerized using Docker. Ready to be deployed to any Docker-compatible hosting environment (AWS ECS, DigitalOcean App Platform, Azure App Service).
- **Frontend**: Standard Vite build (`npm run build`). Can be deployed to static hosting services like Vercel, Netlify, or Cloudflare Pages. The frontend repository also contains an `nginx.conf` and `Dockerfile` for containerized frontend hosting.

---

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.