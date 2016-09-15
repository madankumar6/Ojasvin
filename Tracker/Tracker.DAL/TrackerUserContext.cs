using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tracker.Entities;

namespace Tracker.DAL
{
    public class TrackerUserContext : IdentityDbContext
    {
        //private IConfiguration config;

        public TrackerUserContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TempData>()
              .ToTable("TempUser");
        }

    }
}
