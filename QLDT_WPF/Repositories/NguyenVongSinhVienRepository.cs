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
    public async Task<ApiResponse<List<NguyenVongSinhVienDto>>>
        GetAll()
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

        return new ApiResponse<List<NguyenVongSinhVienDto>>
        {
            Data = list_nguyen_vong,
            StatusCode = 200,
            Status = true,
            Message = "Lấy danh sách nguyện vọng thành công"
        };
    }


    /**
     * Lay nguyen vong cua sinh vien by id
     */
    public async Task<ApiResponse<NguyenVongSinhVienDto>>
        GetById(string id)
    {
        var nguyen_vong = await (
            from nv in _context.DangKyNguyenVongs
            join sv in _context.SinhViens on nv.IdSinhVien equals sv.IdSinhVien
            join mh in _context.MonHocs on nv.IdMonHoc equals mh.IdMonHoc
            where nv.IdDangKyNguyenVong == id
            select new NguyenVongSinhVienDto
            {
                IdNguyenVong = nv.IdDangKyNguyenVong,
                IdSinhVien = nv.IdSinhVien,
                IdMonHoc = mh.IdMonHoc,

                TenSinhVien = sv.HoTen,
                TenMonHoc = mh.TenMonHoc,
                TrangThai = nv.TrangThai,
            }
        ).FirstOrDefaultAsync();

        if (nguyen_vong == null)
        {
            return new ApiResponse<NguyenVongSinhVienDto>
            {
                Data = null,
                StatusCode = 404,
                Status = false,
                Message = "Không tìm thấy nguyện vọng"
            };
        }

        return new ApiResponse<NguyenVongSinhVienDto>
        {
            Data = nguyen_vong,
            StatusCode = 200,
            Status = true,
            Message = "Lấy nguyện vọng thành công"
        };
    }

    /**
     * Lay nguyen vong cua sinh vien by id
     */
    public async Task<ApiResponse<List<NguyenVongSinhVienDto>>>
        GetByIdSinhVien(string idSinhVien)
    {
        var sv = await _context.SinhViens
            .FirstOrDefaultAsync(v => v.IdSinhVien == idSinhVien);
        if (sv == null)
        {
            return new ApiResponse<List<NguyenVongSinhVienDto>>
            {
                Data = null,
                StatusCode = 404,
                Status = false,
                Message = "Không tìm thấy sinh viên"
            };
        }

        var list_nguyen_vong = await (
            from nv in _context.DangKyNguyenVongs
            join mh in _context.MonHocs
                on nv.IdMonHoc equals mh.IdMonHoc
            where nv.IdSinhVien == idSinhVien
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

        return new ApiResponse<List<NguyenVongSinhVienDto>>
        {
            Data = list_nguyen_vong,
            StatusCode = 200,
            Status = true,
            Message = "Lấy danh sách nguyện vọng thành công"
        };
    }

    /**
     * Them nguyen vong cua sinh vien
     */
    public async Task<ApiResponse<NguyenVongSinhVienDto>>
        Add(NguyenVongSinhVienDto dto)
    {
        var sv = await _context.SinhViens
            .FirstOrDefaultAsync(v => v.IdSinhVien == dto.IdSinhVien);
        if (sv == null)
        {
            return new ApiResponse<NguyenVongSinhVienDto>
            {
                Data = null,
                StatusCode = 404,
                Status = false,
                Message = "Không tìm thấy sinh viên"
            };
        }

        var mh = await _context.MonHocs
            .FirstOrDefaultAsync(v => v.IdMonHoc == dto.IdMonHoc);
        if (mh == null)
        {
            return new ApiResponse<NguyenVongSinhVienDto>
            {
                Data = null,
                StatusCode = 404,
                Status = false,
                Message = "Không tìm thấy môn học"
            };
        }

        var nguyen_vong = new DangKyNguyenVong
        {
            IdDangKyNguyenVong = Guid.NewGuid().ToString(),
            IdSinhVien = dto.IdSinhVien,
            IdMonHoc = dto.IdMonHoc,
            TrangThai = -1
        };

        _context.DangKyNguyenVongs.Add(nguyen_vong);
        await _context.SaveChangesAsync();

        return new ApiResponse<NguyenVongSinhVienDto>
        {
            Data = dto,
            StatusCode = 200,
            Status = true,
            Message = "Thêm nguyện vọng thành công"
        };
    }

    /**
     * Xoa nguyen vong cua sinh vien By Id 
     */
    public async Task<ApiResponse<NguyenVongSinhVienDto>>
        Delete(string id)
    {
        var nguyen_vong = await _context.DangKyNguyenVongs
            .FirstOrDefaultAsync(v => v.IdDangKyNguyenVong == id);
        if (nguyen_vong == null)
        {
            return new ApiResponse<NguyenVongSinhVienDto>
            {
                Data = null,
                StatusCode = 404,
                Status = false,
                Message = "Không tìm thấy nguyện vọng"
            };
        }

        _context.DangKyNguyenVongs.Remove(nguyen_vong);
        await _context.SaveChangesAsync();

        return new ApiResponse<NguyenVongSinhVienDto>
        {
            Data = null,
            StatusCode = 200,
            Status = true,
            Message = "Xóa nguyện vọng thành công"
        };
    }

    /**
     * Lay nguyen vong by mon hoc
     */
    public async Task<ApiResponse<List<NguyenVongSinhVienDto>>>
        GetByIdMonHoc(string idMonHoc)
    {
        var mh = await _context.MonHocs
            .FirstOrDefaultAsync(x => x.IdMonHoc == idMonHoc);
        if (mh == null)
        {
            return new ApiResponse<List<NguyenVongSinhVienDto>>
            {
                Data = null,
                StatusCode = 404,
                Status = false,
                Message = "Không tìm thấy môn học"
            };
        }

        var list_nguyen_vong_sinh_vien = await (
            from nv in _context.DangKyNguyenVongs
            join sv in _context.SinhViens
                on nv.IdSinhVien equals sv.IdSinhVien
            join mh in _context.MonHocs
                on nv.IdMonHoc equals mh.IdMonHoc
            where nv.IdMonHoc == idMonHoc
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

        return new ApiResponse<List<NguyenVongSinhVienDto>>
        {
            Data = list_nguyen_vong_sinh_vien,
            StatusCode = 200,
            Status = true,
            Message = "Lấy danh sách nguyện vọng thành công"
        };
    }


    /**
     * Chap nhan nguyen vong by id
     */
    public async Task<ApiResponse<NguyenVongSinhVienDto>>
        Accpet(string id)
    {
        var nguyen_vong = await _context.DangKyNguyenVongs
            .FirstOrDefaultAsync(v => v.IdDangKyNguyenVong == id);
        if (nguyen_vong == null)
        {
            return new ApiResponse<NguyenVongSinhVienDto>
            {
                Data = null,
                StatusCode = 404,
                Status = false,
                Message = "Không tìm thấy nguyện vọng"
            };
        }

        nguyen_vong.TrangThai = 1;
        await _context.SaveChangesAsync();

        return new ApiResponse<NguyenVongSinhVienDto>
        {
            Data = null,
            StatusCode = 200,
            Status = true,
            Message = "Chấp nhận nguyện vọng thành công"
        };
    }

    /**
     * Tu choi nguyen vong by id
     */
    public async Task<ApiResponse<NguyenVongSinhVienDto>>
        Reject(string id)
    {
        var nguyen_vong = await _context.DangKyNguyenVongs
            .FirstOrDefaultAsync(v => v.IdDangKyNguyenVong == id);
        if (nguyen_vong == null)
        {
            return new ApiResponse<NguyenVongSinhVienDto>
            {
                Data = null,
                StatusCode = 404,
                Status = false,
                Message = "Không tìm thấy nguyện vọng"
            };
        }

        nguyen_vong.TrangThai = 0;
        await _context.SaveChangesAsync();

        return new ApiResponse<NguyenVongSinhVienDto>
        {
            Data = null,
            StatusCode = 200,
            Status = true,
            Message = "Chấp nhận nguyện vọng thành công"
        };
    }

}