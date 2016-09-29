using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Tracker.DAL;

namespace Tracker.DAL.Migrations
{
    [DbContext(typeof(UserContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("Tracker.Entities.Identity.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AddressLine3");

                    b.Property<string>("AddressLine4");

                    b.Property<string>("Country");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("PostalCode");

                    b.Property<string>("State");

                    b.Property<int>("UserId");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Tracker.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Tracker.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Tracker.Entities.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Tracker.Entities.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CssClass");

                    b.Property<bool>("Enabled");

                    b.Property<int?>("MenuId");

                    b.Property<int?>("MenuOrder");

                    b.Property<int?>("ParentMenuId");

                    b.Property<string>("Text");

                    b.Property<string>("Url");

                    b.HasKey("MenuItemId");

                    b.HasIndex("MenuId");

                    b.HasIndex("ParentMenuId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("Tracker.Entities.RoleMenu", b =>
                {
                    b.Property<int>("RoleMenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsEnabled");

                    b.Property<int>("MenuId");

                    b.Property<int>("RoleId");

                    b.HasKey("RoleMenuId");

                    b.ToTable("RoleMenus");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Tracker.Entities.Identity.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Tracker.Entities.Identity.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Tracker.Entities.Identity.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Tracker.Entities.Identity.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tracker.Entities.Identity.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tracker.Entities.Identity.Address", b =>
                {
                    b.HasOne("Tracker.Entities.Identity.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tracker.Entities.MenuItem", b =>
                {
                    b.HasOne("Tracker.Entities.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId");

                    b.HasOne("Tracker.Entities.MenuItem", "ParentMenu")
                        .WithMany("SubMenuItems")
                        .HasForeignKey("ParentMenuId");
                });
        }
    }
}
