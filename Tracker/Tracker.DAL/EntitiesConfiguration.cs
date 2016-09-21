using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracker.Entities;

namespace Tracker.DAL
{
    public static class EntitiesConfiguration
    {
        public static void ConfigureEntities(ModelBuilder modelBuilder)
        {
            ConfigureMenu(modelBuilder.Entity<Menu>());
        }

        private static void ConfigureMenu(EntityTypeBuilder<Menu> menuBuilder)
        {
            menuBuilder.HasKey(p => p.MenuId);
        }
    }
}
