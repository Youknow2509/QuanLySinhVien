using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;


namespace QLDT_WPF.Data
{
    public class SessionDbContextFactory : IDesignTimeDbContextFactory<SessionDbContext>
    {
        public SessionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SessionDbContext>();
            // Set the connection string or any other options here
            string connectionStrings = ConfigurationManager.ConnectionStrings["SessionDbConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionStrings);

            return new SessionDbContext(optionsBuilder.Options);
        }
    }
}
