namespace Tracker.DAL.Services
{
    using Microsoft.EntityFrameworkCore;

    using Tracker.Common;

    public class UserDbContextFactory 
    {
        public static UserDbContext Create(TrackerDatabase database, string connectionString)
        {
            DbContextOptionsBuilder<UserDbContext> optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();

            switch (database)
            {
                case TrackerDatabase.SqlServer:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case TrackerDatabase.MySql:
                    break;
                case TrackerDatabase.InMemoryTesting:
                    break;
                case TrackerDatabase.Oracle:
                    break;
                default:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
            }

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}
