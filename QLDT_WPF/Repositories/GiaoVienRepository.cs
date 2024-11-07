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


    /**
     * Sua thong tin giao vien
     */


    /**
     * Them giao vien
     */


    /**
     * Xoa giao vien By Id 
     */


    /**
     * Admin Xử Lí Cập Nhập Mật Khẩu Giáo Viên
     */

    /**
     * Xử Lí Cập Nhập Mật Khẩu Giáo Viên - Từ Chính Giáo Viên
     */


}