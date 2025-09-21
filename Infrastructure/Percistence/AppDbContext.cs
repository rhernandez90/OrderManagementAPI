using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Domain.Entities;

namespace OrderManagementAPI.Infrastructure.Percistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            base.OnModelCreating(modelBuilder);

            // Seed de productos
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = new Guid("e9196394-61d4-4fd0-8fca-daffd301b35f"),
                    Sku = "TSHIRT001",
                    Name = "Playera Básica",
                    Price = 15.99m,
                    Color = "Blanco",
                    Size = "M"
                },
                new Product
                {
                    Id = new Guid("8c708ed6-0def-42af-9b8d-ed84c507ddf2"),
                    Sku = "MUG001",
                    Name = "Taza de Café",
                    Price = 7.50m,
                    Color = "Negro",
                    CapacityMl = 350
                },
                new Product
                {
                    Id = new Guid("53093121-52bd-414c-ac0f-2b799fcd5566"),
                    Sku = "POSTER001",
                    Name = "Poster Motivacional",
                    Price = 12.00m,
                    HeightCm = 60,
                    WidthCm = 40,
                    Paper = "Mate"
                }
            );

            modelBuilder.Entity<Product>()
                .Property(p => p.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);



            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId);
        }
    }
}
