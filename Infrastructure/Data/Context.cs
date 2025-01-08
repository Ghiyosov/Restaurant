using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class Context(DbContextOptions<Context> options):DbContext(options)
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Courier> Couriers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Restaurant>()
            .HasMany(r=>r.Menus)
            .WithOne(m=>m.Restaurant)
            .HasForeignKey(m=>m.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Restaurant>()
            .HasMany(r=>r.Orders)
            .WithOne(o => o.Restaurant)
            .HasForeignKey(o => o.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>()
            .HasMany(m=>m.OrderDetails)
            .WithOne(o => o.Order)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<User>()
            .HasMany(u=>u.Couriers)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<User>()
            .HasMany(m=>m.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}