using Microsoft.EntityFrameworkCore;

namespace upLiftUnity_API.Models
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> option):base(option) { 

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<SupVol_Applications> SupVol_Applications {  get; set; }

        public DbSet<Donations> Donations { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
        }
    }
}
