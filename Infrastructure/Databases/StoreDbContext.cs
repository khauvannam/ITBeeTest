using Domain.Models.Orders;
using Infrastructure.Databases.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases;

public class StoreDbContext : DbContext

{
    public StoreDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DbConfiguration.OrderConfiguration());
        modelBuilder.ApplyConfiguration(new DbConfiguration.OrderDetailConfiguration());
    }
}