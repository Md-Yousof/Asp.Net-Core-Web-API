using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace MDWebCoreAPI.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderMaster> OrderMasters { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.OrderMaster)
                .WithMany(o => o.OrderDetail)
                .HasForeignKey(o => o.OrderId);

            // Seed data for Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Product 1" },
                new Product { ProductId = 2, Name = "Product 2" },
                new Product { ProductId = 3, Name = "Product 3" }
            );

            // Seed data for OrderMasters
            modelBuilder.Entity<OrderMaster>().HasData(
                new OrderMaster
                {
                    OrderId = 1,
                    CustomerName = "John Doe",
                    OrderDate = DateTime.Now,
                    IsComplete = true
                },
                new OrderMaster
                {
                    OrderId = 2,
                    CustomerName = "Jane Smith",
                    OrderDate = DateTime.Now.AddDays(-1),
                    IsComplete = false
                }
            );

            // Seed data for OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    DetailId = 1,
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 1,
                    Price = 100
                },
                new OrderDetail
                {
                    DetailId = 2,
                    OrderId = 1,
                    ProductId = 2,
                    Quantity = 2,
                    Price = 200
                },
                new OrderDetail
                {
                    DetailId = 3,
                    OrderId = 2,
                    ProductId = 3,
                    Quantity = 3,
                    Price = 300
                }
            );
        }
    }
}
