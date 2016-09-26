namespace Tracker.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Entities;
    using Entities.Identity;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class EntitiesConfiguration
    {
        public static void ConfigureEntities(ModelBuilder modelBuilder)
        {
            ConfigureMenu(modelBuilder.Entity<Menu>());
        }

        private static void ConfigureMenu(EntityTypeBuilder<Menu> menuBuilder)
        {
            menuBuilder.HasKey(p => p.MenuId);
            menuBuilder.Property(p => p.MenuId).ValueGeneratedOnAdd();
        }

        private static void ConfigureUser(EntityTypeBuilder<User> menuBuilder)
        {
            menuBuilder.HasKey(p => p.Id);
        }

    }
}
