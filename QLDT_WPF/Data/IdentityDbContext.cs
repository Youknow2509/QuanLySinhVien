
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//
using QLDT_WPF.Models;

namespace QLDT_WPF.Data;

public class IdentityDbContext : IdentityDbContext<UserCustom>
{
    // Variables

    // Constructor
    public IdentityDbContext(
        DbContextOptions<IdentityDbContext> options
    ) : base(options)
    {

    }

    // Cấu hình kết nối với cơ sở dữ liệu nếu cần
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionStrings = ConfigurationManager.ConnectionStrings["IdentityDbConnection"];
            optionsBuilder.UseSqlServer(connectionStrings);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        // Bỏ tiền tố AspNet của các bảng: mặc định
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }

    }
}