using System.Collections.Generic;
using System.Linq;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;
using QLDT_WPF.Services;

namespace QLDT_WPF.Repositories;

public class GiaoVienRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    private readonly SecurityService _securityService;
    private readonly IdentityDbContext _identityContext;

    // Constructor
    public GiaoVienRepository()
    {
        // Connect to database QuanLySinhVienDbContext
        var connectionString = ConfigurationManager.ConnectionStrings["QuanLySinhVienDbConnection"].ConnectionString;
        _context = new QuanLySinhVienDbContext(
            new DbContextOptionsBuilder<QuanLySinhVienDbContext>()
                .UseSqlServer(connectionString)
                .Options);

        // Connect to database IdentityDbContext
        var connectionStringIdentity = ConfigurationManager.ConnectionStrings["IdentityDbConnection"].ConnectionString;
        _identityContext = new IdentityDbContext(
            new DbContextOptionsBuilder<IdentityDbContext>()
                .UseSqlServer(connectionStringIdentity)
                .Options);

        // Init SecurityService
        _securityService = new SecurityService();
    }

    // Dispose
    public void Dispose()
    {
        _context.Dispose();
        _securityService.Dispose();
    }

    /**
     * Lay tat ca giao vien
     */
    public async Task<ApiResponse<List<GiaoVienDto>>> GetAll()
    {
        // Query
        var query = await (
            from gv in _context.GiaoViens
            join k in _context.Khoas on gv.IdKhoa equals k.IdKhoa
            select new GiaoVienDto
            {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                Email = gv.Email,
                SoDienThoai = gv.SoDienThoai,
                IdKhoa = gv.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).ToListAsync();

        return new ApiResponse<List<GiaoVienDto>>
        {
            Data = query,
            Succeeded = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Lay giao vien by id
     */
    public async Task<ApiResponse<GiaoVienDto>> GetById(string id)
    {
        // Query
        var query = await (
            from gv in _context.GiaoViens
            where gv.IdGiaoVien == id
            join k in _context.Khoas on gv.IdKhoa equals k.IdKhoa
            select new GiaoVienDto
            {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                Email = gv.Email,
                SoDienThoai = gv.SoDienThoai,
                IdKhoa = gv.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).FirstOrDefaultAsync();

        // handle the case not found
        if (query == null)
        {
            return new ApiResponse<GiaoVienDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy giáo viên"
            };
        }

        return new ApiResponse<GiaoVienDto>
        {
            Data = query,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Sua thong tin giao vien
     */


    /**
     * Them giao vien
     */
    public async Task<ApiResponse<GiaoVienDto>> Create(GiaoVienDto giaoVien)
    {
        giaoVien.IdGiaoVien = Guid.NewGuid().ToString();

        try
        {
            // Convert the DTO to the entity model, assuming your entity model is GiaoVien
            var giaoVien = new GiaoVien
            {
                IdGiaoVien = newGiaoVien.IdGiaoVien,
                TenGiaoVien = newGiaoVien.TenGiaoVien,
                Email = newGiaoVien.Email,
                SoDienThoai = newGiaoVien.SoDienThoai,
                IdKhoa = newGiaoVien.IdKhoa
            };
            // Check duplicate ID, email, phone number
            if (_context.GiaoViens.Any(gv => gv.IdGiaoVien == giaoVien.IdGiaoVien))
            {
                return new ApiResponse<GiaoVienDto>
                {
                    Data = null,
                    Status = false,
                    Message = "ID giáo viên đã tồn tại."
                };
            }
            if (_context.GiaoViens.Any(gv => gv.Email == giaoVien.Email))
            {
                return new ApiResponse<GiaoVienDto>
                {
                    Data = null,
                    Status = false,
                    Message = "Email đã tồn tại."
                };
            }
            if (_context.GiaoViens.Any(gv => gv.SoDienThoai == giaoVien.SoDienThoai))
            {
                return new ApiResponse<GiaoVienDto>
                {
                    Data = null,
                    Status = false,
                    Message = "Số điện thoại đã tồn tại."
                };
            }
            // Create user identity with default password is 123456
            // Find id role is GiaoVien
            var role = _identityContext.Roles.FirstOrDefault(r => r.Name == "GiaoVien");
            if (role == null)
            {
                return new ApiResponse<GiaoVienDto>
                {
                    Data = null,
                    Status = false,
                    Message = "Không tìm thấy role GiaoVien"
                };
            }

            // Create new user
            var user = new UserCustom
            {
                IdClaim = giaoVien.IdGiaoVien,
                UserName = giaoVien.IdGiaoVien,
                PasswordHash = _securityService.Hash("123"),
                FullName = giaoVien.TenGiaoVien,
                Email = giaoVien.Email,
                PhoneNumber = giaoVien.SoDienThoai,
            };

            // Add role to user
            _identityContext.UserRoles.Add(new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            // Add to database
            _identityContext.Users.Add(user);
            _context.GiaoViens.Add(giaoVien);

            _identityContext.SaveChanges();
            _context.SaveChanges();

            return new ApiResponse<GiaoVienDto>
            {
                Data = newGiaoVien,
                Status = true,
                Message = "Thêm giáo viên thành công"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<GiaoVienDto>
            {
                Data = null,
                Status = false,
                Message = ex.Message
            };
        }
    }


    /**
     * Xoa giao vien By Id 
     */
    public async Task<ApiResponse<GiaoVienDto>> Delete(string id)
    {
        var qr = _context.GiaoViens
            .FirstOrDefault(gv => gv.IdGiaoVien == id);

        if (qr == null)
        {
            return new ApiResponse<GiaoVienDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy giáo viên"
            };
        }

        _context.Remove(qr);
        await _context.SaveChangesAsync();

        return new ApiResponse<GiaoVienDto>
        {
            Data = qr,
            Status = true,
            Message = "Xóa giáo viên thành công"
        };
    }

    /**
     * Admin Xử Lí Cập Nhập Mật Khẩu Giáo Viên
     */

    /**
     * Xử Lí Cập Nhập Mật Khẩu Giáo Viên - Từ Chính Giáo Viên
     */


}