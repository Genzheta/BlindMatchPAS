# BlindMatchPAS

A web-based platform for academic institutions to facilitate matching between student research projects and academic supervisors using blind matching principles.

## Overview

BlindMatchPAS aims to provide a bias-free selection process by anonymizing student identities during the initial supervisor review phase. Matches are made based on the alignment of research interests and technical stacks.

## Architecture

The project is structured using a clean architecture pattern:

- **BlindMatchPAS.Core**: Domain entities and business logic.
- **BlindMatchPAS.Infrastructure**: Data access and external services.
- **BlindMatchPAS.Web**: ASP.NET Core MVC web application.
- **BlindMatchPAS.Tests**: Test suite.

## Technology Stack

- **Backend**: .NET 9.0, EF Core (SQLite)
- **Frontend**: Razor Views, CSS, JS
- **Testing**: xUnit, Jest, Cypress
- **Security**: ASP.NET Core Identity

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Node.js & npm

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Genzheta/BlindMatchPAS.git
   cd BlindMatchPAS
   ```

2. **Build and Run**:
   ```bash
   dotnet build
   dotnet run --project BlindMatchPAS.Web
   ```
   The application will be available at `http://localhost:5000`.

### Default Credentials
- **Admin**: `admin@pas.com` / `Admin@123`

## Testing

### Backend
```bash
dotnet test
```

### Frontend
```bash
cd BlindMatchPAS.Web
npm test
```

### E2E
```bash
cd BlindMatchPAS.Web
npm run cypress:run
```

## Features

- Blind Matching Logic
- Role-Based Dashboards
- Research Area Management
- Audit Logging

---
*Developed by Group CH for the SDT-P Course.*
