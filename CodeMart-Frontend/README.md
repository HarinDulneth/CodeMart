# 🎨 CodeMart Frontend

The client-side application for the **CodeMart** platform—a modern marketplace for software projects. Built with performance, interactivity, and a stunning user experience in mind.

## 📖 Overview

The CodeMart frontend is a Single Page Application (SPA) that provides a rich, responsive, and highly interactive user interface. It connects to the CodeMart .NET Backend API to browse projects, manage carts, process payments securely via Stripe, and authenticate users via Supabase.

## ✨ Key Features

- **Immersive 3D & Animations**: Utilizes Three.js, Framer Motion, and GSAP for beautiful micro-interactions, 3D elements, and smooth page transitions.
- **Smooth Scrolling**: Integrated Lenis for a fluid, momentum-based scrolling experience.
- **Project Browsing & Search**: Dynamic filtering, searching, and viewing of software projects.
- **Secure Checkout**: Seamless integration with Stripe Elements for handling payments safely.
- **Authentication**: JWT & OAuth (Google) authentication powered by Supabase.
- **Responsive Design**: Mobile-first styling utilizing Tailwind CSS.
- **Dark/Light Mode**: Full theming support with `next-themes`.

## 🛠️ Tech Stack

- **Core**: React 18, TypeScript, Vite
- **Routing**: React Router DOM (v7)
- **Styling**: Tailwind CSS
- **UI Components**: Radix UI (Headless components), Lucide React, Tabler Icons
- **Animations & 3D**: Framer Motion, GSAP, Three.js (`@react-three/fiber`, `@react-three/drei`), Lenis
- **Integrations**: Stripe Elements (`@stripe/react-stripe-js`), Supabase Client (`@supabase/supabase-js`), Google OAuth (`@react-oauth/google`)
- **Linting & Formatting**: ESLint

## 🏗️ Folder Structure

```text
src/
├── assets/        # Static assets (images, 3D models)
├── components/    # Reusable UI components (buttons, cards, dialogs)
├── hooks/         # Custom React hooks
├── layouts/       # Page layout components (Navbar, Footer, Sidebars)
├── pages/         # Route components (Home, Marketplace, Cart, Checkout)
├── services/      # API integration and data fetching
├── store/         # Global state management (Zustand/Context)
├── types/         # TypeScript type definitions
└── utils/         # Helper functions and utilities
```

## 🚀 Quick Start (Development)

### Prerequisites

- **Node.js** (v18 or higher)
- **npm** or **yarn**
- Running instance of the **CodeMart Backend API**

### Step 1: Clone and Install

```bash
git clone <your-repository-url>
cd CodeMart/CodeMart-Frontend
npm install
```

### Step 2: Environment Variables

Create a `.env` file in the root of the `CodeMart-Frontend` directory.

```bash
# Copy the example file if available, or create a new one
touch .env
```

Add your specific configuration values:
```env
VITE_API_BASE_URL=http://localhost:8080/api  # Your Backend API URL
VITE_SUPABASE_URL=your_supabase_url
VITE_SUPABASE_ANON_KEY=your_supabase_anon_key
VITE_STRIPE_PUBLISHABLE_KEY=your_stripe_publishable_key
VITE_GOOGLE_CLIENT_ID=your_google_oauth_client_id
```

### Step 3: Run the Development Server

```bash
npm run dev
```

The application will start in development mode, typically accessible at `http://localhost:5173`. Vite provides instant Hot Module Replacement (HMR) for a fast development experience.

## 📦 Production Build & Deployment

To build the application for production:

```bash
npm run build
```

This will generate optimized, minified static files in the `dist/` directory.

### Docker Deployment

The repository includes a `Dockerfile` and `nginx.conf` for serving the production build using Nginx.

```bash
# Build the Docker image
docker build -t codemart-frontend:latest .

# Run the container (maps to port 80)
docker run -d -p 80:80 codemart-frontend:latest
```

## 📚 Additional Documentation

- [CONNECTION_SETUP.md](CONNECTION_SETUP.md) - Details on backend & database connections.

## 🤝 Contributing

1. Create your feature branch (`git checkout -b feature/AmazingUI`)
2. Commit your changes (`git commit -m 'Add some AmazingUI'`)
3. Push to the branch (`git push origin feature/AmazingUI`)
4. Open a Pull Request

## 📄 License

This project is licensed under the MIT License.
