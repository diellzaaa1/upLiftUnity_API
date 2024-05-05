﻿using Microsoft.EntityFrameworkCore;

namespace upLiftUnity_API.Models
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SupVol_Applications> SupVol_Applications { get; set; }
        public DbSet<Donations> Donations { get; set; }
        public DbSet<Calls> Calls { get; set; }
        public DbSet<Rules> Rules { get; set; }
        public DbSet<Schedule> Schedule { get; set; }

        public DbSet<UserActivity> UserActivities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUser(modelBuilder);
            ConfigureCalls(modelBuilder);
            ConfigureRules(modelBuilder);
            ConfigureSchedule(modelBuilder);
            ConfigureUserActivities(modelBuilder);
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
        }

        private void ConfigureCalls(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calls>()
               .HasOne(c => c.User)
               .WithMany()
               .HasForeignKey(c => c.UserId);
        }

        private void ConfigureRules(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rules>()
              .HasOne(c => c.User)
              .WithMany()
              .HasForeignKey(c => c.UserId);
        }

        private void ConfigureSchedule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
             .HasOne(c => c.UserSch)
             .WithMany()
             .HasForeignKey(c => c.UserId);
        }
        private void ConfigureUserActivities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserActivity>()
             .HasOne(c => c.User)
             .WithMany()
             .HasForeignKey(c => c.UserId);
        }
    }
}
