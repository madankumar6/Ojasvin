namespace Tracker.DAL.Services
{
    using Microsoft.EntityFrameworkCore;

    using Tracker.Common;

    public class UserDbContextFactory 
    {
        public static UserDbContext Create(TrackerDatabase database = TrackerDatabase.SqlServer, string connectionString = "Data Source=LAPTOP-NV8CBL0N\\SQLEXPRESS;Initial Catalog=TrackerCore;Trusted_Connection=True;User ID = sa;Password = welcome;Persist Security Info = True;Integrated Security =True;")
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

        public static UserDbContext Create(string database, string connectionString)
        {
            DbContextOptionsBuilder<UserDbContext> optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();

            switch (database)
            {
                case "SqlServer":
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case "MySql":
                    break;
                default:
                    optionsBuilder.UseSqlServer(connectionString);

                    break;
            }
            return new UserDbContext(optionsBuilder.Options);
        }
    }
}
