using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Domain.Entities;

namespace MilkCoPOS.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<InventoryItem> Inventory => Set<InventoryItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<DiningTable> Tables => Set<DiningTable>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);
            entity.Property(o => o.Customer).IsRequired().HasMaxLength(100);
            entity.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(o => o.Status).HasConversion<string>().HasMaxLength(30);
            entity.HasOne<DiningTable>()
                .WithMany()
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(i => i.OrderItemId);
            entity.Property(i => i.Quantity).IsRequired();
        });

        modelBuilder.Entity<DiningTable>(entity =>
        {
            entity.HasKey(t => t.TableId);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(30);
            entity.Property(t => t.Status).HasConversion<string>().HasMaxLength(30);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.PaymentId);
            entity.Property(p => p.Method).HasMaxLength(50);
            entity.Property(p => p.Status).HasMaxLength(30);
            entity.Property(p => p.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(i => i.ItemId);
            entity.Property(i => i.Name).IsRequired().HasMaxLength(120);
            entity.Property(i => i.Unit).HasMaxLength(16);
            entity.Property(i => i.UnitPrice).HasPrecision(18, 2);
        });
    }
}
