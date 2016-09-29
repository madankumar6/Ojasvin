namespace Tracker.DAL.Services
{
    using Microsoft.EntityFrameworkCore;

    using Tracker.Common;

    public class UserContextFactory 
    {
        public static UserContext Create(TrackerDatabase database, string connectionString)
        {
            DbContextOptionsBuilder<UserContext> optionsBuilder = new DbContextOptionsBuilder<UserContext>();

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

            return new UserContext(optionsBuilder.Options);
        }
    }
}
