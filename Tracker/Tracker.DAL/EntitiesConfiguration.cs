namespace Tracker.DAL
{
    using Entities;
    using Entities.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class EntitiesConfiguration
    {
        public static void ConfigureMenu(EntityTypeBuilder<Menu> menuBuilder)
        {
            menuBuilder.ToTable("Menu");
            menuBuilder.HasKey(p => p.MenuId);
            menuBuilder.Property(p => p.MenuId).ValueGeneratedOnAdd();
        }

        public static void ConfigureMenuItem(EntityTypeBuilder<MenuItem> menuItemsBuilder)
        {
            menuItemsBuilder.ToTable("MenuItem");
            menuItemsBuilder.Property(p => p.MenuItemId).ValueGeneratedOnAdd();
        }

        public static void ConfigureUser(EntityTypeBuilder<User> userBuilder)
        {
            userBuilder.ToTable("User");
            userBuilder.HasKey(p => p.Id);
            userBuilder.Property(prop => prop.Id).HasColumnName("UserId");
            userBuilder.Property(p => p.Id).ValueGeneratedOnAdd();
        }

        public static void ConfigureRole(EntityTypeBuilder<Role> roleBuilder)
        {
            roleBuilder.ToTable("Role");
            roleBuilder.HasKey(p => p.Id);
            roleBuilder.Property(prop => prop.Id).HasColumnName("RoleId");
            roleBuilder.Property(p => p.Id).ValueGeneratedOnAdd();
        }

        public static void ConfigureUserRole(EntityTypeBuilder<IdentityUserRole<int>> userRoleBuilder)
        {
            userRoleBuilder.ToTable("UserRole");
        }

        public static void ConfigureUserClaim(EntityTypeBuilder<IdentityUserClaim<int>> userClaimBuilder)
        {
            userClaimBuilder.ToTable("UserClaim");
        }
         
        public static void ConfigureRoleClaim(EntityTypeBuilder<IdentityRoleClaim<int>> roleClaimBuilder)
        {
            roleClaimBuilder.ToTable("RoleClaim");
        }

        public static void ConfigureUserToken(EntityTypeBuilder<IdentityUserToken<int>> userTokenBuilder)
        {
            userTokenBuilder.ToTable("UserToken");
        }

        public static void ConfigureUserLogin(EntityTypeBuilder<IdentityUserLogin<int>> userLoginBuilder)
        {
            userLoginBuilder.ToTable("UserLogin");
        }
    }
}
