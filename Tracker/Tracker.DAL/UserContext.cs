namespace Tracker.DAL
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using Tracker.Entities;
    using Tracker.Entities.Identity;

    public class UserContext : IdentityDbContext<User, Role, int>
    {
        private IConfigurationRoot Configuration { get; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize the ASP.NET Identity model and override the defaults if needed. 
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserToken");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
        }
    }
}
