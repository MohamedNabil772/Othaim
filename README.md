# Othaim Product Catalog

This is a Clean Architecture based Blazor Server solution for managing a Product Catalog with authentication.

## Structure
- `ProductCatalog.API` - Web API with Swagger and Identity endpoints
- `ProductCatalog.Blazor` - Blazor Server frontend UI
- `ProductCatalog.Application` - CQRS Handlers, Interfaces, Validators
- `ProductCatalog.Domain` - Entities and core business logic
- `ProductCatalog.Infrastructure` - EF Core, Identity, In-Memory DB
- `ProductCatalog.Tests` - xUnit based tests

## Setup
```bash
git clone <your-repo-url>
cd Othaim.ProductCatalog
dotnet build
dotnet run --project src/ProductCatalog.Blazor
```

## Auth
- Admin: `admin@test.com` / `Password123!`
