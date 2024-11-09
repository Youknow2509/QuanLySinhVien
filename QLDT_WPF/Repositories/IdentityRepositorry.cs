using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
//
using QLDT_WPF.Data;
using QLDT_WPF.Models;
using QLDT_WPF.Dto;

namespace QLDT_WPF.Repositories
{
    public class IdentityRepository : IDisposable
    {
        private readonly QuanLySinhVienDbContext _context;
        private readonly IdentityDbContext _dbContext;

        public IdentityRepository()
        {
            // Handle connection Quan Ly Sinh Vien Db context
            var connectionString = ConfigurationManager
                .ConnectionStrings["QuanLySinhVienDbConnection"].ConnectionString;
            _context = new QuanLySinhVienDbContext(
                new DbContextOptionsBuilder<QuanLySinhVienDbContext>()
                    .UseSqlServer(connectionString)
                    .Options);

            // Handle connection Identity Db context
            var identityConnectionString = ConfigurationManager
                .ConnectionStrings["IdentityDbConnection"].ConnectionString;
            _dbContext = new IdentityDbContext(
                new DbContextOptionsBuilder<IdentityDbContext>()
                    .UseSqlServer(identityConnectionString)
                    .Options);

        }

        // Dispose
        public void Dispose()
        {
            _context.Dispose();
            _dbContext.Dispose();
        }

        // Get all users
        public async Task<ApiResponse<List<UserDto>>> GetAll()
        {
            var list_users = await _dbContext.Users
                .Include(x => x.UserRoles)
                .Include(x => x.Roles)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    IdClaim = x.IdClaim,
                    UserName = x.UserName,
                    Email = x.Email,
                    Phone = x.PhoneNumber,
                    FullName = x.FullName,
                    Address = x.Address,
                    IdRole = x.UserRoles.FirstOrDefault()?.RoleId,
                    RoleName = x.Roles.FirstOrDefault()?.RoleName
                })
                .ToListAsync();

            return new ApiResponse<List<UserDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Lấy danh sách người dùng thành công.",
                Data = list_users
            };
        }
    
        // Get all sinh vien
        public async Task<ApiResponse<List<SinhVienDto>>> GetAllSinhVien()
        {
            var list_sinhvien = await _context.SinhViens
                .Include(x => x.Khoa)
                .Select(x => new SinhVienDto
                {
                    IdSinhVien = x.IdSinhVien,
                    IdKhoa = x.IdKhoa,
                    IdChuongTrinhHoc = x.IdChuongTrinhHoc,
                    HoTen = x.HoTen,
                    Lop = x.Lop,  
                    NgaySinh = x.NgaySinh,
                    DiaChi = x.DiaChi,
                    TenKhoa = x.Khoa.TenKhoa, 
                    SoDienThoai = x.SoDienThoai,
                    Email = x.Email,
                })
                .ToListAsync();

            return new ApiResponse<List<SinhVienDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Lấy danh sách sinh viên thành công.",
                Data = list_sinhvien
            };
        }
    }
}