<div align="center">

<img src="https://img.shields.io/badge/CodeMart-Frontend-6366f1?style=for-the-badge&logo=react&logoColor=white" alt="CodeMart Frontend" />

# CodeMart Frontend

**The client-side SPA for the CodeMart software marketplace.**

[![React](https://img.shields.io/badge/React_18-61DAFB?style=flat-square&logo=react&logoColor=black)](https://react.dev/)
[![TypeScript](https://img.shields.io/badge/TypeScript_5-3178C6?style=flat-square&logo=typescript&logoColor=white)](https://www.typescriptlang.org/)
[![Vite](https://img.shields.io/badge/Vite_5-646CFF?style=flat-square&logo=vite&logoColor=white)](https://vitejs.dev/)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS_3-06B6D4?style=flat-square&logo=tailwindcss&logoColor=white)](https://tailwindcss.com/)
[![Three.js](https://img.shields.io/badge/Three.js-000000?style=flat-square&logo=three.js&logoColor=white)](https://threejs.org/)
[![Stripe](https://img.shields.io/badge/Stripe-635BFF?style=flat-square&logo=stripe&logoColor=white)](https://stripe.com/)
[![Supabase](https://img.shields.io/badge/Supabase-3ECF8E?style=flat-square&logo=supabase&logoColor=white)](https://supabase.com/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](../LICENSE)

</div>

---

## Overview

The CodeMart frontend is a Single Page Application (SPA) that delivers an immersive, responsive marketplace experience. It interfaces with the [CodeMart .NET Backend API](../CodeMart.Server) to browse software projects, manage carts and orders, process payments securely with Stripe, and authenticate users via Supabase and Google OAuth.

---

## ✨ Features

| Feature | Description |
|---|---|
| 🌐 **Project Marketplace** | Dynamic browsing, filtering, and full-text search across software listings |
| 🎨 **3D & Animations** | Immersive visuals powered by Three.js, `@react-three/fiber`, GSAP, and Framer Motion |
| 🌀 **Smooth Scrolling** | Momentum-based, fluid scroll experience via Lenis |
| 💳 **Stripe Checkout** | Secure, embedded payment flows using Stripe Elements |
| 🔐 **Authentication** | JWT + Google OAuth sign-in powered by Supabase |
| 🌗 **Dark / Light Mode** | Full theme support via `next-themes` |
| 📊 **Data Visualisation** | In-app charts powered by Recharts |
| 🔔 **Toast Notifications** | Clean, accessible toasts via Sonner |
| 📱 **Fully Responsive** | Mobile-first layout built with Tailwind CSS |
| 🐳 **Docker Ready** | Production Nginx container image for one-command deployment |

---

## 💻 Tech Stack

### Core

| Category | Technology | Version |
|---|---|---|
| Framework | React | ^18.3 |
| Language | TypeScript | ^5.5 |
| Build Tool | Vite | ^5.4 |
| Routing | React Router DOM | ^7.9 |
| Styling | Tailwind CSS | ^3.4 |

### UI & Components

| Category | Technology |
|---|---|
| Headless Components | Radix UI (Accordion, Dialog, Dropdown, Select, Tabs, Toast, …) |
| Icons | Lucide React, Tabler Icons, React Icons |
| Carousel | Embla Carousel |
| Class Utilities | clsx, tailwind-merge, class-variance-authority |

### Animations & 3D

| Technology | Purpose |
|---|---|
| Three.js + `@react-three/fiber` + `@react-three/drei` | 3D scene rendering |
| GSAP + `@gsap/react` | Timeline & scroll-driven animations |
| Framer Motion (`motion`) | Declarative React animations |
| Lenis + `@studio-freight/lenis` | Smooth momentum scrolling |
| Cobe | Interactive globe component |
| three-globe | WebGL globe visualisation |

### Integrations

| Technology | Purpose |
|---|---|
| `@stripe/react-stripe-js` + `@stripe/stripe-js` | Embedded payment UI |
| `@supabase/supabase-js` | Auth, storage, and realtime |
| `@react-oauth/google` | Google One-Tap / OAuth login |
| Recharts | Charting and data visualisation |
| Sonner | Toast notification system |
| next-themes | Dark/light theme management |

---

## 🏗️ Project Structure

```
CodeMart-Frontend/
├── public/                   # Static public assets
├── src/
│   ├── assets/               # Images, 3D models, and media files
│   ├── components/           # Reusable UI components (buttons, cards, dialogs, …)
│   ├── data/                 # Static data and mock content
│   ├── hooks/                # Custom React hooks
│   ├── lib/                  # API clients and shared utilities
│   ├── pages/                # Route-level page components
│   │   ├── Home/
│   │   ├── Marketplace/
│   │   ├── Cart/
│   │   ├── Checkout/
│   │   └── ...
│   ├── services/             # HTTP service layer (API calls)
│   ├── utils/                # Helper functions
│   ├── App.tsx               # Root application component & routes
│   ├── main.tsx              # Application entry point
│   └── index.css             # Global styles
├── Dockerfile                # Production Docker image (Nginx)
├── nginx.conf                # Nginx configuration for SPA routing
├── vite.config.ts            # Vite development config
├── vite.config.prod.ts       # Vite production config
├── tailwind.config.js        # Tailwind CSS configuration
└── tsconfig.json             # TypeScript configuration
```

---

## 🚀 Quick Start

### Prerequisites

- **Node.js** v18 or higher
- **npm** v9 or higher
- A running instance of the **[CodeMart Backend API](../CodeMart.Server)** (defaults to `http://localhost:8080`)
- Supabase project credentials
- Stripe publishable key

---

### 1. Install Dependencies

From the repository root:

```bash
cd CodeMart-Frontend
npm install
```

---

### 2. Configure Environment Variables

Create a `.env` file in the `CodeMart-Frontend/` directory:

```env
# Backend API
VITE_API_BASE_URL=http://localhost:8080/api

# Supabase
VITE_SUPABASE_URL=https://your-project.supabase.co
VITE_SUPABASE_ANON_KEY=your-supabase-anon-key

# Stripe (Publishable key — safe to expose in frontend)
VITE_STRIPE_PUBLISHABLE_KEY=pk_test_...

# Google OAuth
VITE_GOOGLE_CLIENT_ID=your-google-client-id.apps.googleusercontent.com
```

> **Note:** Never commit `.env` to version control. It is listed in `.gitignore` by default.

---

### 3. Start the Development Server

```bash
npm run dev
```

The app will start at **`http://localhost:5173`** with Vite's instant Hot Module Replacement (HMR).

---

## 📜 Available Scripts

| Script | Command | Description |
|---|---|---|
| **Dev server** | `npm run dev` | Starts Vite dev server with HMR on port 5173 |
| **Production build** | `npm run build` | Outputs optimised static files to `dist/` |
| **Preview build** | `npm run preview` | Serves the production build locally for testing |
| **Lint** | `npm run lint` | Runs ESLint across the entire source tree |

---

## 📦 Production Build & Deployment

### Static Hosting (Vercel / Netlify / Cloudflare Pages)

```bash
npm run build
```

Deploy the generated `dist/` directory to your preferred static host. Ensure you configure your host to redirect all routes to `index.html` for SPA routing to work correctly.

### Docker (Nginx)

The included `Dockerfile` builds the app and serves it via Nginx — ideal for containerised environments:

```bash
# Build the image
docker build -t codemart-frontend:latest .

# Run the container (exposed on port 80)
docker run -d -p 80:80 codemart-frontend:latest
```

The bundled `nginx.conf` is pre-configured to handle SPA client-side routing correctly.

---

## 📚 Additional Documentation

| Document | Description |
|---|---|
| [CONNECTION_SETUP.md](CONNECTION_SETUP.md) | Backend API & database connection configuration |
| [../DOCKER_SETUP.md](../DOCKER_SETUP.md) | Full Docker Compose setup for the entire stack |
| [../SUPABASE_SETUP.md](../SUPABASE_SETUP.md) | Supabase project and auth configuration guide |

---

## 🤝 Contributing

1. **Fork** the repository
2. **Create** a feature branch: `git checkout -b feature/your-feature-name`
3. **Commit** your changes using conventional commits: `git commit -m 'feat: add amazing UI component'`
4. **Push** to your branch: `git push origin feature/your-feature-name`
5. **Open a Pull Request** against the `main` branch

Please ensure:
- No TypeScript errors (`tsc --noEmit`)
- ESLint passes (`npm run lint`)
- UI changes are tested across mobile and desktop viewports

---

## 📄 License

This project is licensed under the **MIT License** — see the [LICENSE](../LICENSE) file for details.

---

<div align="center">

Part of the [CodeMart](https://github.com/HarinDulneth/CodeMart) monorepo · Made with ❤️ by [HarinDulneth](https://github.com/HarinDulneth)

</div>
