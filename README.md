# 🎲 BoardGame Catalog

## Overview

This project is a simple full-stack web application for managing a board game catalog.

---

## Features

* View all board games
* Create a new board game
* Edit existing board games
* Delete board games
* Full CRUD functionality via REST API
* Docker-based execution
* CI pipeline with automatic image build and push

---

## Tech Stack

### Backend

* ASP.NET Core Web API (.NET)
* C#

### Frontend

* Vite
* TypeScript
* Vanilla JavaScript (no framework)

### Database

* MongoDB

### DevOps

* Docker & Docker Compose
* GitHub Actions (CI)
* GitHub Container Registry (ghcr.io)

---

## Architecture

```
Frontend (Vite + TypeScript)
        ↓ HTTP (REST API)
Backend (ASP.NET Core)
        ↓
MongoDB
```

Containerized version:

```
Frontend (Nginx, port 5173)
        ↓
Backend (ASP.NET, port 5178)
        ↓
MongoDB
```

---

## Project Structure

```
.
├── src/
│   ├── backend/
│   │   └── BoardGame.Api/
│   └── frontend/
│       └── boardgame-app/
├── docker-compose.yml
├── docker-compose.ci.yml
├── .github/
│   └── workflows/
│       └── ci.yml
└── README.md
```

---

## Getting Started

### Option 1 – Run with Docker (recommended)

```bash
docker compose up --build
```

After startup:

* Frontend: http://localhost:5173
* Backend: http://localhost:5178
* API health check: http://localhost:5178/api/health

---

### Option 2 – Run from prebuilt images (CI)

```bash
docker compose -f docker-compose.ci.yml up --pull always
```

This will:

* pull images from GitHub Container Registry
* start the full system without local build

---

### Option 3 – Local development (optional)

#### Backend

```bash
cd src/backend/BoardGame.Api
dotnet run
```

#### Frontend

```bash
cd src/frontend/boardgame-app
npm install
npm run dev
```

#### Database

```bash
docker run -d -p 27017:27017 --name mongodb mongo
```

---

## Prerequisites

Required:

* Docker Desktop

Optional (for local development):

* .NET SDK
* Node.js + npm

---

## 🔌 API Endpoints

| Method | Endpoint             | Description         |
| ------ | -------------------- | ------------------- |
| GET    | /api/boardgames      | Get all board games |
| GET    | /api/boardgames/{id} | Get by id           |
| POST   | /api/boardgames      | Create new          |
| PUT    | /api/boardgames/{id} | Update              |
| DELETE | /api/boardgames/{id} | Delete              |

---

## Usage

1. Open the frontend in your browser
2. View the list of board games
3. Add a new game using the form
4. Edit existing entries
5. Delete entries

All actions are performed via API calls.

---

## CI Pipeline

The project includes a GitHub Actions workflow that:

* builds backend and frontend Docker images
* pushes them to GitHub Container Registry (ghcr.io)

Published images:

* `ghcr.io/drbilbobaggins/boardgame-backend`
* `ghcr.io/drbilbobaggins/boardgame-frontend`

The CI pipeline runs on:

* push to `main`
* pull requests
* manual trigger

---

## Known Limitations

* No authentication or authorization
* No pagination or filtering
* Minimal frontend validation
* Basic UI (not production-ready)
