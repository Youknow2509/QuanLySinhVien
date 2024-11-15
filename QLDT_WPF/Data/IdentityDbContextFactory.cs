using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;


namespace QLDT_WPF.Data
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            // Set the connection string or any other options here
            string connectionStrings = ConfigurationManager.ConnectionStrings["IdentityDbConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionStrings);

            return new IdentityDbContext(optionsBuilder.Options);
        }
    }
}
