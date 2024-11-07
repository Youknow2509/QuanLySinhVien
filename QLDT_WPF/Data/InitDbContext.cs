
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

//
using QLDT_WPF.Models;
using QLDT_WPF.Helpers;

namespace QLDT_WPF.Data;

public class InitDbContext
{
    // Initialize database for Identity
    public static void Initialize(
        IServiceProvider serviceProvider
    )
    {
        using (var context = new IdentityDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
        {
            context.Database.EnsureCreated();

            // Create user root account
            if (!context.Users.Any())
            {
                // User root
                var admin = new UserCustom
                {
                    UserName = "admin",
                    Email = "root@v.com",
                    EmailConfirmed = true,
                    ProfilePicture = null,
                    FirstName = "Admin",
                    LastName = "Vip Pro",
                    FullName = "Vinh",
                    Address = "HN",
                    Phone = "088888888"
                };

                var password = "123";
                var passwordHash = "i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY="; // SecurityHelper.Hash(password);
                admin.PasswordHash = passwordHash;

                // Save changes
                context.Users.AddRange(admin);
                context.SaveChanges();
            }

            // Create roles for Identity 
            if (!context.Roles.Any())
            {
                var rootRole = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                var adminRole = new IdentityRole
                {
                    Name = "SinhVien",
                    NormalizedName = "SINHVIEN"
                };

                var userRole = new IdentityRole
                {
                    Name = "GiaoVien",
                    NormalizedName = "GIAOVIEN"
                };

                context.Roles.AddRange(rootRole, adminRole, userRole);

                context.SaveChanges();
            }

            // Add user to roles
            if (!context.UserRoles.Any())
            {
                var adminUser = context.Users.FirstOrDefault(u => u.UserName == "admin");

                var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");

                context.UserRoles.AddRange(
                    new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = adminRole.Id }
                );
                context.SaveChanges();
            }
            context.SaveChanges();
        }
    }
}