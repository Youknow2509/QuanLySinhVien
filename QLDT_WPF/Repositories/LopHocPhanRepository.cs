using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using Azure.Identity;

namespace QLDT_WPF.Repositories;

public class LopHocPhanRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public LopHocPhanRepository()
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
     * Lay tat ca lop hoc phan
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>> GetAll()
    {
        var lhp = await (
            from l in _context.LopHocPhans
            join gv in _context.GiaoViens 
                on l.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs
                on l.IdMonHoc equals mh.IdMonHoc
            select new LopHocPhanDto
            {
                IdLopHocPhan = l.IdLopHocPhan,
                IdMonHoc = l.IdMonHoc,
                IdGiaoVien = l.IdGiaoVien,

                TenLopHocPhan = l.TenHocPhan,
                TenGiaoVien= gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                ThoiGianBatDau = l.ThoiGianBatDau,
                ThoiGianKetThuc = l.ThoiGianKetThuc,
            }
        ).ToListAsync();

        return new ApiResponse<List<LopHocPhanDto>>{
            Data = lhp,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Lay lop hoc phan by id
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>> GetById(string id)
    {
        var list_lhp = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == id
            join gv in _context.GiaoViens 
                on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs
                on lhp.IdMonHoc equals mh.IdMonHoc
            select new LopHocPhanDto{
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdMonHoc = lhp.IdMonHoc,
                IdGiaoVien = lhp.IdGiaoVien,

                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien= gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc,
            }
        ).ToListAsync();

        return new ApiResponse<List<LopHocPhanDto>>{
            Data = list_lhp,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Sua thong tin lop hoc phan
     */
    public async Task<ApiResponse<LopHocPhanDto>> Edit(LopHocPhanDto lopHocPhan)
    {
        var lhp = await _context.LopHocPhans
            .FirstOrDefaultAsync(l => l.IdLopHocPhan == lopHocPhan.IdLopHocPhan);
        if (lhp == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        // check giao vien, mon hoc ton tai
        var gv = await _context.GiaoViens
            .FirstOrDefaultAsync(g => g.IdGiaoVien == lopHocPhan.IdGiaoVien);
        if (gv == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy giáo viên"
            };
        }
        var mh = await _context.MonHocs
            .FirstOrDefaultAsync(m => m.IdMonHoc == lopHocPhan.IdMonHoc);
        if (mh == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy môn học"
            };
        }

        // check thoi gian thay doi
        if ((lhp.ThoiGianBatDau != lopHocPhan.ThoiGianBatDau
            || lhp.ThoiGianKetThuc != lopHocPhan.ThoiGianKetThuc)
            && lhp.ThoiGianBatDau >= DateTime.Now)
        {
            return new ApiResponse<LopHocPhanDto>{
                Data = null,
                Status = false,
                Message = "Không thể thay đổi thời gian lớp học phần khi lớp học phần đã diễn ra"
            };
        }

        // update lop hoc phan
        lhp.TenHocPhan = lopHocPhan.TenLopHocPhan;
        lhp.IdGiaoVien = lopHocPhan.IdGiaoVien;
        lhp.IdMonHoc = lopHocPhan.IdMonHoc;
        lhp.ThoiGianBatDau = lopHocPhan.ThoiGianBatDau;
        lhp.ThoiGianKetThuc = lopHocPhan.ThoiGianKetThuc;

        await _context.SaveChangesAsync();

        return new ApiResponse<LopHocPhanDto>
        {
            Data = lopHocPhan,
            Status = true,
            Message = "Sửa thông tin lớp học phần thành công"
        };
    }

    /**
     * Them lop hoc phan
     */
    public async Task<ApiResponse<LopHocPhanDto>> Add(LopHocPhanDto lopHocPhan)
    {
        // if id lop hoc phan is null -> generate new id
        // TODO
        return null;
    }

    /**
     * Xoa lop hoc phan By Id 
     */
    public async Task<ApiResponse<LopHocPhanDto>> Delete(string id)
    {
        // TODO
        return null;
    }

    /** 
     * Get lop hoc phan cua sinh vien tu id
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>> GetLopHocPhansFromSinhVien(string id)
    {
        // TODO 
        return null;
    }

    /** 
     * Get lop hoc phan cua giao vien tu id
     */

    /**
     * Get lop hoc phan tu id mon hoc
     */

    /**
     * Thay doi thoi gian lop hoc phan 
     */

    /**
     * Thêm thời gian cho lớp học phần
     */

    // Helper check thoi gian
    private async Task<ApiResponse<LopHocPhanDto>> checkThoiGian(LopHocPhanDto lopHocPhan)
    {
        // TODO
        return null;
    }
}