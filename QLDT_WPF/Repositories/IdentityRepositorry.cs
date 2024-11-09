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
using QLDT_WPF.Views.Login;
using QLDT_WPF.Services;

namespace QLDT_WPF.Repositories
{
    public class IdentityRepository : IDisposable
    {
        private readonly QuanLySinhVienDbContext _context;
        private readonly IdentityDbContext _dbContext;
        private readonly SecurityService _securityService;

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

            // Init security service
            _securityService = new SecurityService();
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
            var list_users = await (
                from user in _dbContext.Users
                join userRole in _dbContext.UserRoles
                    on user.Id equals userRole.UserId
                join role in _dbContext.Roles
                    on userRole.RoleId equals role.Id
                select new UserDto
                {
                    Id = user.Id,
                    IdClaim = user.IdClaim,
                    UserName = user.UserName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    FullName = user.FullName,
                    Address = user.Address,
                    IdRole = role.Id,
                    RoleName = role.Name,
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
            var list_sinhvien = await (
                from x in _context.SinhViens
                join y in _context.Khoas
                    on x.IdKhoa equals y.IdKhoa
                join z in _dbContext.Users
                    on x.IdSinhVien equals z.IdClaim
                select new SinhVienDto
                {
                    IdSinhVien = x.IdSinhVien,
                    IdKhoa = x.IdKhoa,
                    IdChuongTrinhHoc = x.IdChuongTrinhHoc,
                    HoTen = x.HoTen,
                    Lop = x.Lop,
                    NgaySinh = x.NgaySinh,
                    DiaChi = x.DiaChi,
                    TenKhoa = y.TenKhoa,
                    SoDienThoai = z.PhoneNumber,
                    Email = z.Email,
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

        // Get all giao vien
        public async Task<ApiResponse<List<GiaoVienDto>>> GetAllGiaoVien()
        {
            var list_gv = await (
                from gv in _context.GiaoViens
                join khoa in _context.Khoas
                    on gv.IdKhoa equals khoa.IdKhoa
                select new GiaoVienDto
                {
                    IdGiaoVien = gv.IdGiaoVien,
                    TenGiaoVien = gv.TenGiaoVien,
                    Email = gv.Email,
                    SoDienThoai = gv.SoDienThoai,
                    IdKhoa = gv.IdKhoa,
                    TenKhoa = khoa.TenKhoa,
                }
            ).ToListAsync();

            return new ApiResponse<List<GiaoVienDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Lấy danh sách giáo viên thành công.",
                Data = list_gv
            };
        }

        // Create admin user
        public async Task<ApiResponse<UserDto>> CreateAdminUser(string username, string password)
        {
            // Add user
            var user = new UserCustom
            {
                UserName = username,
                PasswordHash = _securityService.Hash(password),
            };
            var result = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // Add role
            var adminRole = _dbContext.Roles.FirstOrDefault(x => x.Name.ToUpper() == "ADMIN");
            if (adminRole == null)
            {
                return new ApiResponse<UserDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Không tìm thấy role ADMIN.",
                    Data = null
                };
            }

            var userRole = new UserRoles
            {
                UserId = user.Id,
                RoleId = adminRole.Id,
            };

            await _dbContext.UserRoles.AddAsync(userRole);
            await _dbContext.SaveChangesAsync();

            return new ApiResponse<UserDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Tạo người dùng thành công.",
                Data = userDto
            };
        }
    }
}