# WordSmith Hub

## A modern platform for freelance translators

WordSmith Hub is a full‑stack application designed to streamline the daily workflow of freelance translators. It centralizes client management, project tracking, work orders, invoicing, and administrative data in a clean, secure, and efficient environment.

This project was developed as part of the requirements to validate the Titre Professionnel Concepteur Développeur d’Applications (CDA).

## 🎯 Why This Project?

As a freelance translator myself, I identified the need for a unified tool to manage clients, projects, rates, and invoices.  
This project allowed me to demonstrate the full range of CDA competencies while solving a real professional problem.

## 🚀 Project Overview

Freelance translators often juggle multiple tools to manage clients, projects, rates, invoices, and legal information. WordSmith Hub brings all these elements together into a unified platform with:

- A structured domain model aligned with real translation workflows
- A secure authentication and user management system
- A clean separation between domain, application, and infrastructure layers
- A modern, maintainable backend architecture (DDD + Vertical Slice)
- A PostgreSQL database with explicit modeling of translation‑specific entities

## 🧩 Key Features

### 👤 User Management

- Account creation and authentication
- Complete freelance profile setup
  - Personal information
  - Legal status
  - Bank details
  - Languages (source/target)
  - Services offered

### 🧾 Client & Project Management

- Create and manage direct clients
- Define custom rates per client
- Create projects associated with a client
- Add work orders linked to a project

### 💼 Invoicing Workflow

- Generate invoices for completed work orders
- Modify invoices before sending
- Track invoice status

### 🛠️ Administration

- Manage static reference tables:
  - Legal statuses
  - Languages
  - Currencies
  - Services

### 🏗️ Architecture

WordSmith Hub follows a clean, maintainable architecture inspired by:

- Domain‑Driven Design (DDD)
- Vertical Slice Architecture
- EF Core for ORM
- FastEndpoints for API endpoints
- FluentValidation for validation
- PostgreSQL as the database engine
- Dependency Injection (DI) for clean separation of concerns

This structure ensures:

- Clear separation of concerns
- Explicit domain modeling
- High testability
- Reduced coupling between features

#### 📁 Project Structure

```bash
src/
├── WordSmithHub.Api/             # FastEndpoints API, commands, queries, validators
├── WordSmithHub.Domain/          # Entities, value objects, domain logic
├── WordSmithHub.Infrastructure/  # EF Core, repositories, migrations
└── WordSmithHub.Tests/           # Unit tests
```

### 🗃️ Database Modeling

The database includes entities such as:

- Freelance
- DirectCustomer
- Project
- WorkOrder
- Invoice
- Rate
- TranslationLanguage
- LegalStatus
- BankAccount
- Owned types (e.g., Address) and indexes are used to optimize queries and maintain domain purity.

### 🛡️ Security

The project integrates:

- Secure password hashing
- JWT‑based authentication
- Sensitive data handling aligned with best practices
- Clear separation between public and private data

### 🧪 Testing & Quality

- Unit tests for domain logic
- Validation rules integrated into endpoints
- Database migrations with EF Core
- Dockerized environment for consistent development
- Qodana for code quality analysis

### 📦 Tech Stack

WordSmith Hub is built with **.NET 10**, the latest stable release of the .NET platform.

| Layer | Technologies |
| --- | ---------------------------------------- |
| Backend | .NET 10, FastEndpoints, EF Core |
| Database | PostgreSQL, Docker |
| Architecture | DDD, Vertical Slice |
| Tools | FluentValidation, JWT Auth, Migrations |

## ▶️ Getting Started

### Prerequisites

- .NET 10 SDK
- Docker & Docker Compose
- PostgreSQL 16+
- EF Core CLI tools

### Running the project

```bash
docker compose --env-file .env.dev -f compose.dev.yaml up
dotnet ef database update --context IdentityDbContext
dotnet ef database update --context MainDbContext
dotnet run --project src/WordSmithHub.Api
```

### Environment variables

Create a .env.dev file with:

```bash
# Database
POSTGRES_PASSWORD=your_secure_password_here
POSTGRES_USER=postgres
POSTGRES_APP_DB=myappdb
POSTGRES_APP_DB_PORT=5433
POSTGRES_IDENTITY_DB=identitydb
POSTGRES_IDENTITY_DB_PORT=5432
POSTGRES_HOST=db

# JWT
JWT_KEY=your_jwt_secret_key_here_min_32_chars
JWT_ISSUER=MyAppBackend
JWT_AUDIENCE=MyAppClient
JWT_ACCESSTOKENSECONDS=600
```

### 📚 Project Goals (CDA Certification)

This project was built to demonstrate the full range of competencies required for the Titre Professionnel CDA, including:

- Designing and modeling a complete application
- Implementing secure authentication
- Building a maintainable backend architecture
- Managing relational data with a robust schema
- Producing documentation (LLD, data dictionary, UML, user stories)
- Ensuring code quality and maintainability

### 📄 Roadmap

- [ ] Add dashboard & analytics
- [ ] Implement invoice PDF generation
- [ ] Add email notifications
- [ ] Add multi‑currency support
- [ ] Add UI/Frontend (React)

### 👤 Author

**François Popieul**  
Freelance Translator & Software Developer  
Based in Grenoble, France

## 📜 License

This project is released under the MIT License.

![License](https://img.shields.io/badge/license-MIT-green)
![.NET](https://img.shields.io/badge/.NET-10.0-blue)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-blue)
![Status](https://img.shields.io/badge/status-active-brightgreen)
