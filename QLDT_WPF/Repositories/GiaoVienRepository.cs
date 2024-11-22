using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;
using QLDT_WPF.Services;
using System.Windows;
using Syncfusion.Data.Extensions;

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
        _identityContext.Dispose();
    }

    /**
     * Lay tat ca giao vien
     */
    public async Task<ApiResponse<List<GiaoVienDto>>> GetAll()
    {
        // Query
        var query = await (
            from gv in _context.GiaoViens
            join k in _context.Khoas 
                on gv.IdKhoa equals k.IdKhoa
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
            Status = true,
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
    public async Task<ApiResponse<GiaoVienDto>> Edit(GiaoVienDto giaoVien)
    {
        try
        {
            // Convert the DTO to the entity model, assuming your entity model is GiaoVien
            var gv = new GiaoVien
            {
                IdGiaoVien = giaoVien.IdGiaoVien,
                TenGiaoVien = giaoVien.TenGiaoVien,
                Email = giaoVien.Email,
                SoDienThoai = giaoVien.SoDienThoai,
                IdKhoa = giaoVien.IdKhoa
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

            // Update to database
            _context.GiaoViens.Update(gv);
            await _context.SaveChangesAsync();

            return new ApiResponse<GiaoVienDto>
            {
                Data = giaoVien,
                Status = true,
                Message = "Sửa thông tin giáo viên thành công"
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
            Data = null,
            Status = true,
            Message = "Xóa giáo viên thành công"
        };
    }


    /**
     *     * Lay toan bo sinh vien thuoc cac lop cua giao vien
     * */

    public async Task<ApiResponse<List<SinhVienDto>>> GetSinhVienByGiaoVienId(string id){
        var query = await (
            from gv in _context.GiaoViens
            join lop in _context.LopHocPhans on gv.IdGiaoVien equals lop.IdGiaoVien
            join lophocphan in _context.SinhVienLopHocPhans on lop.IdLopHocPhan equals lophocphan.IdLopHocPhan
            join sv in _context.SinhViens on lophocphan.IdSinhVien equals sv.IdSinhVien
            join k in _context.Khoas on sv.IdKhoa equals k.IdKhoa
            join cth in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cth.IdChuongTrinhHoc
            where gv.IdGiaoVien == id
            select new SinhVienDto
            {
                IdSinhVien = sv.IdSinhVien,
                HoTen = sv.HoTen,
                NgaySinh = sv.NgaySinh,
                Lop = lop.TenHocPhan,
                TenChuongTrinhHoc = cth.TenChuongTrinhHoc,
                TenKhoa = k.TenKhoa,
                DiaChi = sv.DiaChi,
            }
        ).ToListAsync();

        return new ApiResponse<List<SinhVienDto>>
        {
            Data = query,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }
}