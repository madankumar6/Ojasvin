﻿namespace Tracker.DAL
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using Tracker.Entities;
    using Tracker.Entities.Identity;

    public class UserDbContext : IdentityDbContext<User, Role, int>
    {
        private IConfigurationRoot Configuration { get; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize the ASP.NET Identity model and override the defaults if needed. 
            modelBuilder.Entity<User>(EntitiesConfiguration.ConfigureUser);
            modelBuilder.Entity<Role>(EntitiesConfiguration.ConfigureRole);
            modelBuilder.Entity<IdentityUserRole<int>>(EntitiesConfiguration.ConfigureUserRole);
            modelBuilder.Entity<IdentityUserClaim<int>>(EntitiesConfiguration.ConfigureUserClaim);
            modelBuilder.Entity<IdentityUserToken<int>>(EntitiesConfiguration.ConfigureUserToken);
            modelBuilder.Entity<IdentityUserLogin<int>>(EntitiesConfiguration.ConfigureUserLogin);
            modelBuilder.Entity<IdentityRoleClaim<int>>(EntitiesConfiguration.ConfigureRoleClaim);
        }
    }
}
