# 📦 Installation Guide (INSTALL.md)

## Goal

This guide explains how to run the application using:

* `git clone`
* prebuilt Docker images from GitHub Container Registry
* `docker-compose.ci.yml`

This is the **recommended way** to run the project, because it does not require local build, ensures consistent environment and uses the CI pipeline artifacts.

---

## Prerequisites

Make sure the following is installed:

* **Git** (required for cloning the repository)
* **Docker Desktop**

Check installation:

```bash
docker --version
docker compose version
git --version
```

---

## 1. Clone the repository

```bash
git clone https://github.com/drbilbobaggins/beadando_alkfet.git
cd beadando_alkfet
```

Alternatively, you can download only the `docker-compose.ci.yml` file and use it directly,
but cloning the repository is recommended to ensure compatibility and version consistency.

---

## 2. Run the application

```bash
docker compose -f docker-compose.ci.yml up --pull always
```

### What this does:

* pulls the latest images from **ghcr.io**
* starts all required services:

  * MongoDB
  * Backend
  * Frontend
* connects the services automatically

---

## 3. Access the application

After startup:

* **Frontend:** http://localhost:5173
* **Backend API:** http://localhost:5178
* **Health check:** http://localhost:5178/api/health

---

## 4. Stop the application

```bash
docker compose -f docker-compose.ci.yml down
```
