using System.Collections.Generic;
using System.Linq;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;

namespace QLDT_WPF.Repositories;

public class DiemRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public DiemRepository()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["QuanLySinhVienDbConnection"].ConnectionString;
        _context = new QuanLySinhVienDbContext(
            new DbContextOptionsBuilder<QuanLySinhVienDbContext>()
                .UseSqlServer(connectionString)
                .Options);
    }

    /**
     * Lay tat ca diem
     */
    public Task<ApiResponse<List<DiemDto>>> GetAll()
    {
        // Query
        var query = await(
            from d in _context.Diems
            join lhp in _context.LopHocPhans
                on d.IdLopHocPhan equals lhp.IdLopHocPhan
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            select new DiemDto
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
                TenLopHocPhan = lhp.TenHocPhan,
            }
        ).ToListAsync();

        // Directly return the JSON result
        return new ApiResponse<List<DiemDto>>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = query
        };
    }
    
    /**
     * Lay diem by id
     */
    public Task<ApiResponse<DiemDto>> GetById(string id)
    {
         // Query
        var query = await (
            from d in _context.Diems
            join lhp in _context.LopHocPhans
                on d.IdLopHocPhan equals lhp.IdLopHocPhan
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            where d.IdDiem == id
            select new DiemDto
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
                TenLopHocPhan = lhp.TenHocPhan,
            }
        ).FirstOrDefault();

        return new ApiResponse<DiemDto>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = query
        };
    }

    /**
     * Lay diem by id sinh vien
     */


    /**
     * Sua thong tin diem
     */


    /**
     * Them diem
     */


    /**
     * Xoa diem By Id 
     */

    /**
     * Trả Về Danh Sách Điểm Của Sinh Viên Với Lần Thi Cuối Cùng, 
     * Có Điểm Tổng Kết >= 7, Chưa Có Trong Nguyện Vọng
     */

    /**
     * Lay diem by id lop hoc phan
     */

    /**
     * Nhập điểm cho sinh viên
     */

    /**
     * Nhap 'List' Diem Sinh Vien
     */


}