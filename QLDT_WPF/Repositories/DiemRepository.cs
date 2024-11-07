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
        var query = await(
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
    public Task<ApiResponse<List<DiemDto>>> GetByIdSinhVien(string id)
    {
        // Query
        var query = await(
            from d in _context.Diems
            where d.IdSinhVien == idSinhVien
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

        return new ApiResponse<List<DiemDto>>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = query
        };
    }

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
    public async Task<ApiResponse<List<DiemDto>>> GetDiemDangKyNguyenVongFromSinhVien(string idSinhVien)
    {
        // Lấy các môn học có điểm tổng kết >= 7 và lần học là lớn nhất
        var query = await (
            from diem in _context.Diems
            join lopHocPhan in _context.LopHocPhans
                on diem.IdLopHocPhan equals lopHocPhan.IdLopHocPhan
            join monhoc in _context.MonHocs
                on lopHocPhan.IdMonHoc equals monhoc.IdMonHoc
            where diem.IdSinhVien == idSinhVien && diem.DiemTongKet <= 7
            join latestDiem in (
                from d in _context.Diems
                where d.IdSinhVien == idSinhVien && d.DiemTongKet <= 7
                group d by d.IdLopHocPhan into g
                select new
                {
                    IdLopHocPhan = g.Key,
                    MaxLanHoc = g.Max(x => x.LanHoc)
                }
            ) on new { diem.IdLopHocPhan, diem.LanHoc } equals new { latestDiem.IdLopHocPhan, LanHoc = latestDiem.MaxLanHoc }
            select new DiemDto
            {
                IdDiem = diem.IdDiem,
                IdSinhVien = diem.IdSinhVien,
                IdLopHocPhan = diem.IdLopHocPhan,
                IdMon = monhoc.IdMonHoc,
                TenLopHocPhan = lopHocPhan.TenHocPhan,
                DiemQuaTrinh = diem.DiemQuaTrinh,
                DiemKetThuc = diem.DiemKetThuc,
                DiemTongKet = diem.DiemTongKet,
                LanHoc = diem.LanHoc,
                TenMonHoc = monhoc.TenMonHoc,
            }
        ).ToListAsync();

        // lay cac mon hoc da dang ky cua sinh vien
        var queryDangKy = await (
            from dk in _context.DangKyNguyenVongs
            where dk.IdSinhVien == idSinhVien
            select dk.IdMonHoc
        ).ToListAsync();

        // Loại bỏ các môn học đã đăng ký
        query = query.Where(x => !queryDangKy.Contains(x.IdMonHoc)).ToList();

        return new ApiResponse<List<DiemDto>>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = query
        };
    }

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