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

        public DbSet<Calls> Calls { get; set; }

        public DbSet<Rules> Rules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Calls>()
               .HasOne(c => c.User)
               .WithMany()
               .HasForeignKey(c =>c.UserId);

            modelBuilder.Entity<Rules>()
              .HasOne(c => c.User)
              .WithMany()
              .HasForeignKey(c => c.UserId);
        }
    }
}
