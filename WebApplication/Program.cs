using MilkCoPOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Application.Ports;
using MilkCoPOS.Application.Services;
using MilkCoPOS.Data;
using MilkCoPOS.Infrastructure.Persistence;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection is not configured.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IOrderRepositoryPort, OrderRepositoryAdapter>();
builder.Services.AddScoped<IInventoryRepositoryPort, InventoryRepositoryAdapter>();
builder.Services.AddScoped<IPaymentRepositoryPort, PaymentRepositoryAdapter>();
builder.Services.AddScoped<ITableRepositoryPort, TableRepositoryAdapter>();

builder.Services.AddScoped<IOrderUseCaseService, OrderUseCaseService>();
builder.Services.AddScoped<IInventoryUseCaseService, InventoryUseCaseService>();
builder.Services.AddScoped<IPaymentUseCaseService, PaymentUseCaseService>();
builder.Services.AddScoped<ITableUseCaseService, TableUseCaseService>();
builder.Services.AddScoped<IReportingUseCaseService, ReportingUseCaseService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    if (!dbContext.Tables.Any())
    {
        dbContext.Tables.AddRange(new[]
        {
            new DiningTable { Name = "T1", SeatCount = 4 },
            new DiningTable { Name = "T2", SeatCount = 4 },
            new DiningTable { Name = "T3", SeatCount = 2 },
            new DiningTable { Name = "T4", SeatCount = 2 },
            new DiningTable { Name = "T5", SeatCount = 6 },
            new DiningTable { Name = "T6", SeatCount = 6 },
            new DiningTable { Name = "T7", SeatCount = 8 },
            new DiningTable { Name = "T8", SeatCount = 8 }
        });
        dbContext.SaveChanges();
    }
}

app.UseStaticFiles();

app.MapGet("/", context =>
{
    context.Response.Redirect("/orders");
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "order-page",
    pattern: "orders/{action=Index}/{id?}",
    defaults: new { controller = "OrderPage" });

app.MapControllers();

app.Run();
