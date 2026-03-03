# MilkCO FnB Management API (.NET 10 / C# 14)

Repo này được tối giản cho bài toán quản lý FnB của nhà hàng MilkCO (40 chỗ ngồi):
- Quản lý tồn kho nguyên vật liệu.
- Quản lý đơn hàng và trừ kho tự động.
- Quản lý thanh toán.
- Tài liệu dự án theo 49 process PMP để AI Agent có thể dùng trực tiếp.

## 1) Công nghệ
- C# 14
- .NET 10 (`net10.0`)
- ASP.NET Core Web API
- Entity Framework Core 10 + SQL Server
- Docker + Docker Compose

## 2) Cấu trúc repo (đã review và tối giản)
- `WebApplication/Controllers`: API cho Health, Orders, Inventory, Payments.
- `WebApplication/Models`: Domain model cho FnB.
- `WebApplication/Data`: `ApplicationDbContext`.
- `WebApplication/Repositories`, `WebApplication/Services`: nghiệp vụ tạo đơn và trừ tồn kho.
- `docs/pmp-processes`: 49 file Markdown theo 49 process PMP.
- `Dockerfile`, `docker-compose.yml`: chạy môi trường bằng Docker.

## 3) Chạy bằng Docker (khuyến nghị)
```bash
docker compose up --build -d
```

API mặc định:
- Health: `GET http://localhost:8080/api/health`
- Orders: `GET http://localhost:8080/api/orders`
- Inventory: `GET http://localhost:8080/api/inventory`
- Payments: `GET http://localhost:8080/api/payments`

Dừng môi trường:
```bash
docker compose down
```

## 4) Chạy local bằng .NET SDK 10
```bash
dotnet restore HelloWorldMvc.sln
dotnet build HelloWorldMvc.sln
dotnet run --project WebApplication/WebApplication.csproj
```

## 5) Tài liệu PMP cho AI Agent
- Danh mục: `docs/pmp-processes/`
- Mỗi file có:
  - Metadata
  - Inputs / Tools & Techniques / Outputs
  - Prompt seed để AI Agent tự động hỗ trợ PM/BA
