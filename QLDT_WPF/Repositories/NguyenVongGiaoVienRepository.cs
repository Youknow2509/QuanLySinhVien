using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;

namespace QLDT_WPF.Repositories;

public class NguyenVongGiaoVienRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public NguyenVongGiaoVienRepository()
    {
        // Connect to database QuanLySinhVienDbContext
        var connectionString = ConfigurationManager.ConnectionStrings["QuanLySinhVienDbConnection"].ConnectionString;
        _context = new QuanLySinhVienDbContext(
            new DbContextOptionsBuilder<QuanLySinhVienDbContext>()
                .UseSqlServer(connectionString)
                .Options);
    }

    // Dispose
    public void Dispose()
    {
        _context.Dispose();
    }

    /**
     * Lay tat ca nguyen vong cua giao vien
     */
    public async Task<ApiResponse<List<NguyenVongThayDoiLichDto>>> GetAll()
    {
        var list_nguyen_vong = await (
            from nguyenvong in _context.DangKyDoiLichs
            join tg_lhp in _context.ThoiGianLopHocPhans
                on nguyenvong.IdThoiGian equals tg_lhp.IdThoiGian
            join lhp in _context.LopHocPhans
                on tg_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
            select new NguyenVongThayDoiLichDto
            {
                IdDangKyDoiLich = nguyenvong.IdDangKyDoiLich,
                IdThoiGian = nguyenvong.IdThoiGian,
                IdLopHocPhan = lhp.IdLopHocPhan,

                ThoiGianBatDauHienTai = nguyenvong.ThoiGianBatDauHienTai,
                ThoiGianKetThucHienTai = nguyenvong.ThoiGianKetThucHienTai,
                ThoiGianBatDauMoi = nguyenvong.ThoiGianBatDauMoi,
                ThoiGianKetThucMoi = nguyenvong.ThoiGianKetThucMoi,

                TrangThai = nguyenvong.TrangThai,
                TenLopHocPhan = lhp.TenHocPhan,
            }
            ).ToListAsync();

        return new ApiResponse<List<NguyenVongThayDoiLichDto>>
        {
            Data = list_nguyen_vong,
            Success = true,
            Message = "Lấy danh sách nguyện vọng thành công"
        };
    }

    /**
     * Lay nguyen vong cua giao vien by id
     */

    /**
     * Lay nguyen vong cua giao vien by id
     */


    /**
     * Sua thong tin nguyen vong cua giao vien
     */


    /**
     * Them nguyen vong cua giao vien
     */


    /**
     * Xoa nguyen vong cua giao vien By Id 
     */

    /**
     * Lay nguyen vong by id giao vien
     */

    /**
     * Chap nhan nguyen vong by id
     */

    /**
     * Tu choi nguyen vong by id
     */

}