using Microsoft.EntityFrameworkCore;
using qlsv.Models;

namespace qlsv.Data
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