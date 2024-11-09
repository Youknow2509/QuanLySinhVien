using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
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

            var userRole = new IdentityUserRole<string>
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
                Data = new UserDto
                {
                    UserName = username,
                }
            };
        }

        // Create sinh vien
        public async Task<ApiResponse<SinhVienDto>> CreateSinhVienUser(SinhVienDto sinhVien, string password)
        {
            // Add user
            var user = new UserCustom
            {
                UserName = sinhVien.IdSinhVien,
                IdClaim = sinhVien.IdSinhVien,
                Email = sinhVien.Email,
                PhoneNumber = sinhVien.SoDienThoai,
                FullName = sinhVien.HoTen,
                Address = sinhVien.DiaChi,
                PasswordHash = _securityService.Hash(password),
            };
            // Check if user, email, or phone number already exists
            var checkResult = await CheckExist(sinhVien.Email, sinhVien.Email, sinhVien.SoDienThoai);
            if (checkResult.status)
            {
                return new ApiResponse<SinhVienDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = checkResult.message,
                    Data = null
                };
            }
            var result = await _dbContext.Users.AddAsync(user);
            // Add role
            var sinhVienRole = _dbContext.Roles
                .FirstOrDefault(x => x.Name.ToUpper() == "SINHVIEN");
            var userRole = new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = sinhVienRole.Id,
            };
            await _dbContext.UserRoles.AddAsync(userRole);
            // add sinh vien
            var sv = new SinhVien
            {
                IdSinhVien = sinhVien.IdSinhVien,
                IdKhoa = sinhVien.IdKhoa,
                IdChuongTrinhHoc = sinhVien.IdChuongTrinhHoc,
                HoTen = sinhVien.HoTen,
                Lop = sinhVien.Lop,
                NgaySinh = sinhVien.NgaySinh,
                DiaChi = sinhVien.DiaChi,
            };
            // Check khoa, chuong trinh hoc
            var khoa = await _context.Khoas.FirstOrDefaultAsync(x => x.IdKhoa == sinhVien.IdKhoa);
            if (khoa == null)
            {
                return new ApiResponse<SinhVienDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Không tìm thấy khoa.",
                    Data = null
                };
            }
            var cth = await _context.ChuongTrinhHocs.FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == sinhVien.IdChuongTrinhHoc);
            if (cth == null)
            {
                return new ApiResponse<SinhVienDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Không tìm thấy chương trình học.",
                    Data = null
                };
            }
            var result_sv = await _context.SinhViens.AddAsync(sv);

            // Save changes
            await _dbContext.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return new ApiResponse<SinhVienDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Tạo sinh viên thành công.",
                Data = sinhVien
            };
        }

        // create giao vien
        public async Task<ApiResponse<GiaoVienDto>> CreateGiaoVienUser(GiaoVienDto giaoVien, string password)
        {
            // Add user
            var user = new UserCustom
            {
                UserName = giaoVien.IdGiaoVien,
                IdClaim = giaoVien.IdGiaoVien,
                Email = giaoVien.Email,
                PhoneNumber = giaoVien.SoDienThoai,
                FullName = giaoVien.TenGiaoVien,
                PasswordHash = _securityService.Hash(password),
            };
            // Check if user, email, or phone number already exists
            var checkResult = await CheckExist(giaoVien.Email, giaoVien.Email, giaoVien.SoDienThoai);
            if (checkResult.status)
            {
                return new ApiResponse<GiaoVienDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = checkResult.message,
                    Data = null
                };
            }
            var result = await _dbContext.Users.AddAsync(user);
            // Add role
            var giaoVienRole = _dbContext.Roles
                .FirstOrDefault(x => x.Name.ToUpper() == "GIAOVIEN");
            var userRole = new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = giaoVienRole.Id,
            };
            await _dbContext.UserRoles.AddAsync(userRole);
            // add giao vien
            var gv = new GiaoVien
            {
                IdGiaoVien = giaoVien.IdGiaoVien,
                TenGiaoVien = giaoVien.TenGiaoVien,
                Email = giaoVien.Email,
                SoDienThoai = giaoVien.SoDienThoai,
                IdKhoa = giaoVien.IdKhoa,
            };
            // Check khoa
            var khoa = await _context.Khoas.FirstOrDefaultAsync(x => x.IdKhoa == giaoVien.IdKhoa);
            if (khoa == null)
            {
                return new ApiResponse<GiaoVienDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Không tìm thấy khoa.",
                    Data = null
                };
            }
            var result_gv = await _context.GiaoViens.AddAsync(gv);

            // Save changes
            await _dbContext.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return new ApiResponse<GiaoVienDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Tạo giáo viên thành công.",
            };
        }

        // Handle login
        public async Task<(bool status, string message)> Login(string username, string password)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.UserName.ToUpper() == username.ToUpper());
            if (user == null)
            {
                return (false, "Tên đăng nhập không tồn tại.");
            }

            if (!_securityService.ValidateHash(password, user.PasswordHash))
            {
                return (false, "Mật khẩu không đúng.");
            }

            return (true, "");
        }

        // admin edit password sinh vien, giao vien
        public async Task<ApiResponse<UserDto>> AdminEditPassword(string id, string password)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.IdClaim == id);
            if (user == null)
            {
                return new ApiResponse<UserDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Không tìm thấy người dùng.",
                    Data = null
                };
            }

            user.PasswordHash = _securityService.Hash(password);
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return new ApiResponse<UserDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Đổi mật khẩu thành công.",
                Data = new UserDto
                {
                    UserName = user.UserName,
                }
            };
        }

        // user edit password
        public async Task<ApiResponse<UserDto>> UserEditPassword(string id, string oldPassword, string newPassword)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.IdClaim == id);
            if (user == null)
            {
                return new ApiResponse<UserDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Không tìm thấy người dùng.",
                    Data = null
                };
            }

            if (!_securityService.ValidateHash(oldPassword, user.PasswordHash))
            {
                return new ApiResponse<UserDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Mật khẩu cũ không đúng.",
                    Data = null
                };
            }

            user.PasswordHash = _securityService.Hash(newPassword);
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return new ApiResponse<UserDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Đổi mật khẩu thành công.",
                Data = new UserDto
                {
                    UserName = user.UserName,
                }
            };
        }

        // Helper check user name, email, phone number exist
        private async Task<(bool status, string message)> CheckExist(string username, string email, string phone)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
            if (user != null)
            {
                return (true, "Tên đăng nhập đã tồn tại.");
            }

            user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                return (true, "Email đã tồn tại.");
            }

            user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == phone);
            if (user != null)
            {
                return (true, "Số điện thoại đã tồn tại.");
            }

            return (false, "");
        }
    }
}