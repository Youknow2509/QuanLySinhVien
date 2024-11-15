using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;


namespace QLDT_WPF.Data
{
    public class QuanLySinhVienDbContextFactory : IDesignTimeDbContextFactory<QuanLySinhVienDbContext>
    {
        public QuanLySinhVienDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuanLySinhVienDbContext>();
            // Set the connection string or any other options here
            string connectionStrings = ConfigurationManager.ConnectionStrings["QuanLySinhVienDbConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionStrings);

            return new QuanLySinhVienDbContext(optionsBuilder.Options);
        }
    }
}
