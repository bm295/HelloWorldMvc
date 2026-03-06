using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Data;
using MilkCoPOS.Repositories;
using MilkCoPOS.Services;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection is not configured.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
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
