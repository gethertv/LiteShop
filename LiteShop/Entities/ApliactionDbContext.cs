using Microsoft.EntityFrameworkCore;
using LiteShop.Entities;
using System.Diagnostics;

namespace LiteShop.Entities
{
    public class ApliactionDbContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Test50;Trusted_Connection=True;");

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Customer> Customer { get; set; } = default!;

        public DbSet<Order>? Order { get; set; }

        public DbSet<Product>? Product { get; set; }

        public DbSet<OrderDetail>? OrderDetail { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products);

            modelBuilder.Entity<OrderDetail>().HasKey(g => new { g.OrderId, g.ProductId, g.OrderDetailId });
        }

    }
}
