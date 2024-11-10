using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;

namespace QLDT_WPF.Repositories;

public class NguyenVongSinhVienRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public NguyenVongSinhVienRepository()
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
     * Lay tat ca nguyen vong cua sinh vien
     */
    public async Task<ApiResponse<List<NguyenVongSinhVienDto>>> GetAll()
    {
        var list_nguyen_vong = await (
            from nv in _context.DangKyNguyenVongs
            join sv in _context.SinhViens on nv.IdSinhVien equals sv.IdSinhVien
            join mh in _context.MonHocs on nv.IdMonHoc equals mh.IdMonHoc
            select new NguyenVongSinhVienDto
            {
                IdNguyenVong = nv.IdDangKyNguyenVong,
                IdSinhVien = nv.IdSinhVien,
                IdMonHoc = mh.IdMonHoc,
                
                TenSinhVien = sv.HoTen,
                TenMonHoc = mh.TenMonHoc,
                TrangThai = nv.TrangThai,
            }
        ).ToListAsync();

        return new ApiResponse<List<NguyenVongSinhVienDto>>{
            Data = list_nguyen_vong,
            StatusCode = 200,
            Status = true,
            Message = "Lấy danh sách nguyện vọng thành công"
        };
    }


    /**
     * Lay nguyen vong cua sinh vien by id
     */

    /**
     * Lay nguyen vong cua sinh vien by id
     */


    /**
     * Sua thong tin nguyen vong cua sinh vien
     */


    /**
     * Them nguyen vong cua sinh vien
     */


    /**
     * Xoa nguyen vong cua sinh vien By Id 
     */

    /**
     * Lay nguyen vong by id sinh vien
     */

    /**
     * Chap nhan nguyen vong by id
     */

    /**
     * Tu choi nguyen vong by id
     */

}