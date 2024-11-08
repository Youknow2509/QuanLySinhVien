using Microsoft.EntityFrameworkCore;
using QLDT_WPF.Models;
using System.Configuration;

namespace QLDT_WPF.Data
{
    public class SessionDbContext : DbContext
    {
        // Variables
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        // Constructors
        public SessionDbContext(DbContextOptions<SessionDbContext> options)
            : base(options)
        {

        }

        // Cấu hình kết nối với cơ sở dữ liệu nếu cần
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionStrings = ConfigurationManager.ConnectionStrings["SessionDbConnection"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionStrings);
            }
        }

        // 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccessToken>().HasKey(m => m.Id);
            builder.Entity<AccessToken>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<RefreshToken>().HasKey(m => m.Id);
            builder.Entity<RefreshToken>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
        }
    }
}