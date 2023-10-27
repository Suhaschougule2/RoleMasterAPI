using Microsoft.EntityFrameworkCore;
using RoleMasterAPI.Model;

namespace RoleMasterAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique constraint on the Name property of the Role entity
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Additional configurations if needed

            base.OnModelCreating(modelBuilder);
        }
    }

}
