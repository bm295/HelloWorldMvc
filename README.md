# HelloWorldMvc (MilkCoPOS API)

## 1. Muc tieu
Huong dan setup local de chay project, tao database, tao schema bang, va nap du lieu mac dinh.

## 2. Yeu cau moi truong + setup lan dau (theo step)
- .NET SDK 9.0
- SQL Server LocalDB (hoac SQL Server bat ky)
- SQL Server Management Studio (SSMS) de quan ly DB bang giao dien
- `dotnet-ef` CLI

Step 1 - Kiem tra nhanh:

```powershell
dotnet --version
dotnet ef --version
```

Step 2 - Cai dat theo thu tu:
1. Cai SQL Server LocalDB.
2. Cai SSMS.

Step 3 - Mo SSMS va ket noi LocalDB:
- `Server type`: `Database Engine`
- `Server name`: `(localdb)\MSSQLLocalDB`
- `Authentication`: `Windows Authentication`

Step 4 - Kiem tra `DefaultConnection` trong `WebApplication/appsettings.json` dang tro toi `MilkCoPOSDb`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MilkCoPOSDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Step 5 - Tao DB + schema lan dau bang migration (khong can tao DB thu cong trong SSMS):

```powershell
dotnet ef database update `
  --project WebApplication/WebApplication.csproj `
  --context MilkCoPOS.Data.ApplicationDbContext
```

Step 6 - Quay lai SSMS, `Refresh` muc `Databases` de thay `MilkCoPOSDb`.

## 3. Cau hinh ket noi DB
File: `WebApplication/appsettings.json`

Mac dinh:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MilkCoPOSDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Neu dung SQL Server khac, sua `DefaultConnection` theo may cua ban.

## 4. Khoi dong project local

```powershell
dotnet restore HelloWorldMvc.sln
dotnet build HelloWorldMvc.sln
dotnet run --project WebApplication/WebApplication.csproj
```

Mac dinh app chay tai:
- `https://localhost:5001`
- `http://localhost:5000`

API mau:
- `GET /api/orders`
- `GET /api/inventory`
- `GET /api/payments`

## 5. Tao DB bang Entity Framework (khuyen nghi)
Luu y: repo hien co migration cu cho `MvcMovieContext` (bang `Movie`). Phan API hien tai su dung `ApplicationDbContext` voi cac bang Orders/OrderItems/Inventory/Payments.

Tao migration cho context hien tai:

```powershell
dotnet ef migrations add InitialMilkCoPOS `
  --project WebApplication/WebApplication.csproj `
  --context MilkCoPOS.Data.ApplicationDbContext `
  --output-dir Migrations/ApplicationDb
```

Ap dung migration vao DB:

```powershell
dotnet ef database update `
  --project WebApplication/WebApplication.csproj `
  --context MilkCoPOS.Data.ApplicationDbContext
```

## 6. SQL schema tao bang (tham khao)
Schema mac dinh: `dbo`

```sql
CREATE TABLE [dbo].[Orders] (
    [OrderId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Customer] NVARCHAR(100) NOT NULL,
    [Timestamp] DATETIME2 NOT NULL
);

CREATE TABLE [dbo].[Inventory] (
    [ItemId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(120) NOT NULL,
    [Quantity] INT NOT NULL
);

CREATE TABLE [dbo].[Payments] (
    [PaymentId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [OrderId] INT NOT NULL,
    [Amount] DECIMAL(18,2) NOT NULL,
    [Method] NVARCHAR(50) NULL,
    [Status] NVARCHAR(30) NULL
);

CREATE TABLE [dbo].[OrderItems] (
    [OrderItemId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [OrderId] INT NOT NULL,
    [InventoryItemId] INT NOT NULL,
    [Quantity] INT NOT NULL,
    CONSTRAINT [FK_OrderItems_Orders_OrderId]
        FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([OrderId]) ON DELETE CASCADE
);

CREATE INDEX [IX_OrderItems_OrderId] ON [dbo].[OrderItems]([OrderId]);
```

## 7. Seed data mac dinh cho bang
Chay script sau sau khi da tao bang:

```sql
INSERT INTO [dbo].[Inventory] ([Name], [Quantity]) VALUES
(N'Sua tuoi 1L', 120),
(N'Sua chua khong duong', 80),
(N'Pho mai lat', 60),
(N'Banh mi sandwich', 150);

INSERT INTO [dbo].[Orders] ([Customer], [Timestamp]) VALUES
(N'Nguyen Van A', SYSUTCDATETIME()),
(N'Tran Thi B', SYSUTCDATETIME());

INSERT INTO [dbo].[OrderItems] ([OrderId], [InventoryItemId], [Quantity]) VALUES
(1, 1, 2),
(1, 4, 1),
(2, 2, 3);

INSERT INTO [dbo].[Payments] ([OrderId], [Amount], [Method], [Status]) VALUES
(1, 75000, N'Cash', N'Paid'),
(2, 120000, N'BankTransfer', N'Pending');
```

## 8. Goi API de kiem tra nhanh
Lay danh sach ton kho:

```powershell
curl http://localhost:5000/api/inventory
```

Tao don hang:

```powershell
curl -X POST http://localhost:5000/api/orders `
  -H "Content-Type: application/json" `
  -d '{
    "customer": "Le Van C",
    "items": [
      { "inventoryItemId": 1, "quantity": 2 },
      { "inventoryItemId": 2, "quantity": 1 }
    ]
  }'
```

## 9. Ghi chu
- `SeedData.cs` trong `WebApplication/Models/SeedData.cs` la code seed cua module Movie cu, khong duoc goi trong startup hien tai.
- Neu muon seed tu dong khi app start, can them logic seed cho `ApplicationDbContext` trong `Program.cs`/`Startup.cs`.
