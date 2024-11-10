using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;

namespace QLDT_WPF.Repositories;

public class SinhVienRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    private readonly SecurityService _securityService;
    private readonly IdentityDbContext _identityContext;

    // Constructor
    public SinhVienRepository()
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
     * Lay tat ca sinh vien
     */


    /**
     * Lay sinh vien by id
     */


    /**
     * Sua thong tin sinh vien
     */


    /**
     * Xoa Sinh Vien By Id Sinh Vien
     */


    /**
     * Sinh Vien Thuoc Lop Hoc Phan
     * @param idLopHocPhan
     */

    /**
     * Admin Xử Lí Cập Nhập Mật Khẩu Sinh Viên
     */

    /**
     * Xử Lí Cập Nhập Mật Khẩu Sinh Viên - Từ Chính Sinh Viên
     */


}