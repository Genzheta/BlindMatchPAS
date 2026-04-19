# BlindMatchPAS

A secure, web-based platform designed for academic institutions to facilitate fair and interest-driven matching between student research projects and academic supervisors using **Blind Matching** principles.

## 🚀 Overview

BlindMatchPAS ensures a bias-free selection process by anonymizing student identities during the initial supervisor review phase. Matches are made based on the alignment of research interests and technical stacks, ensuring the best possible fit for both parties.

## 🏗️ Architecture

The project follows a **Clean Architecture** pattern, divided into four main layers:

- **BlindMatchPAS.Core**: Contains domain entities (ApplicationUser, ProjectProposal, ResearchArea, Match, AuditLog), interfaces, and business logic.
- **BlindMatchPAS.Infrastructure**: Implements data access using Entity Framework Core (SQLite), database migrations, and core services.
- **BlindMatchPAS.Web**: The ASP.NET Core MVC application providing the user interface, controllers, and middleware (including Audit Logging).
- **BlindMatchPAS.Tests**: Comprehensive test suite including Unit, Integration, and E2E tests.

## 🛠️ Technology Stack

- **Backend**: .NET 9.0, Entity Framework Core (SQLite)
- **Frontend**: ASP.NET Core MVC (Razor), Vanilla CSS, JavaScript
- **Testing**: Jest (Unit), xUnit (Unit/Integration), Cypress (E2E)
- **Security**: ASP.NET Core Identity with Role-Based Access Control (RBAC)

## 🚦 Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js & npm](https://nodejs.org/) (for JavaScript tests)

### Installation & Run

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Genzheta/BlindMatchPAS.git
   cd BlindMatchPAS
   ```

2. **Restore and Build**:
   ```bash
   dotnet build
   ```

3. **Run the application**:
   ```bash
   dotnet run --project BlindMatchPAS.Web
   ```
    The application will be available at `http://localhost:5000`.

4. **Default Credentials**:
   - **Admin**: `admin@pas.com` / `Admin@123`

## 🧪 Testing

### C# Tests
Run the back-end test suite:
```bash
dotnet test
```

### JavaScript Tests
Run the front-end unit tests using Jest:
```bash
cd BlindMatchPAS.Web
npm test
```

### E2E Tests
Run the end-to-end tests using Cypress:
```bash
cd BlindMatchPAS.Web
# Open Cypress Test Runner
npm run cypress:open
# Run tests in headless mode
npm run cypress:run
```

## 📝 Features

- **Blind Matching Logic**: Anonymized proposal reviews for supervisors.
- **Role-Based Dashboards**: Tailored experiences for Students, Supervisors, and Module Leaders.
- **Research Area Management**: Categorization of projects for better matching.
- **Audit Logging**: Secure tracking of all matching activities.
- **Automated Seeding**: Quick environment setup with pre-defined roles and data.

## 🤝 Contributing

This project is part of a university coursework. Contributions and distribution are managed via the team workflow scripts.

---
*Developed by the Group CH team for the SDT-P Course.*
