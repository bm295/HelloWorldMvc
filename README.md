# MilkCO FnB Management

MilkCO is a small FnB management sample for:
- inventory tracking
- order capture with stock validation and stock deduction
- payment tracking
- PMP project documents under `docs/`

The app includes a web order page on `/orders` and APIs aligned to domain use cases (orders, kitchen, payments, tables, inventory, and reporting).

## Stack
- C# 14
- .NET 10 (`net10.0`)
- ASP.NET Core
- Entity Framework Core 10 + SQL Server
- Docker + Docker Compose


## Architecture
The repository now follows a ports-and-adapters style:
- `Application/Ports`: repository port contracts
- `Application/Services`: use-case orchestration
- `Infrastructure/Persistence`: EF Core adapters implementing ports
- `Controllers`: API adapters calling use-case services only

## Main routes
- Order page: `/orders`
- Health API: `GET /api/health`
- Orders API: `GET /api/orders`
- Add order item: `POST /api/orders/{orderId}/items`
- Remove order item: `DELETE /api/orders/{orderId}/items/{orderItemId}`
- Send order to kitchen: `POST /api/orders/{orderId}/send-to-kitchen`
- Close order: `POST /api/orders/{orderId}/close`
- Inventory API: `GET /api/inventory`
- Payments API: `GET /api/payments`, `POST /api/payments/orders/{orderId}`
- Tables API: `GET /api/tables`
- Reporting API: `GET /api/reports/operations-summary`

## Run with Docker
Starts SQL Server and the app together.

```bash
docker compose up --build -d
```

Database connection in this mode:
- The `api` container connects to SQL Server using the Compose service name `sqlserver`, not `localhost`.
- The connection string is configured in `docker-compose.yml` through `ConnectionStrings__DefaultConnection`.
- The value used by the app container is:

```text
Server=sqlserver,1433;Database=MilkCoPOSDb;User Id=sa;Password=Your_strong_password123;TrustServerCertificate=True;MultipleActiveResultSets=true
```

If you need to change the database host, port, or credentials for Docker-to-Docker startup, update the `ConnectionStrings__DefaultConnection` value under the `api` service in `docker-compose.yml`.

Then open:
- `http://localhost:8080/orders`

Stop the environment:

```bash
docker compose down
```

## Run locally with .NET SDK 10
Make sure SQL Server is available at the connection string in `WebApplication/appsettings.json`.

If you want to use SQL Server from Docker while running the app locally, start only the database container:

```bash
docker compose up -d sqlserver
```

Then set `ConnectionStrings:DefaultConnection` in `WebApplication/appsettings.json` to:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,14333;Database=MilkCoPOSDb;User Id=sa;Password=Your_strong_password123;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

```bash
dotnet restore HelloWorldMvc.sln
dotnet build HelloWorldMvc.sln
dotnet run --project WebApplication/WebApplication.csproj
```

Then open:
- `http://localhost:5000/orders`
- `https://localhost:5001/orders`

## Order page flow
1. Add inventory items first if the catalog is empty.
2. Open `/orders`.
3. Enter the customer name.
4. Set quantities for one or more inventory items.
5. Submit the form to create the order.
6. Review the confirmation page and the saved API record if needed.

## Project documents
- Charter and supporting documents: `docs/01-project-charter/`
- PMP process library: `docs/pmp-processes/`
