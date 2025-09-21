using Microsoft.EntityFrameworkCore;

namespace OrderManagementAPI.Infrastructure.Percistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // configuraciones Fluent API si necesitas
        }
    }
}
