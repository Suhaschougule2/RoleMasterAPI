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

      
    }

}
