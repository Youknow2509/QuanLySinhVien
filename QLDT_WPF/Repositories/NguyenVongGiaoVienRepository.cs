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
            Status = true,
            Message = "Lấy danh sách nguyện vọng thành công",
            StatusCode = 200,
        };
    }

    /**
     * Lay nguyen vong cua giao vien by id
     */
    public async Task<ApiResponse<NguyenVongThayDoiLichDto>> GetById(string id)
    {
        var nguyen_vong = await (
            from nguyenvong in _context.DangKyDoiLichs
            join tg_lhp in _context.ThoiGianLopHocPhans
                on nguyenvong.IdThoiGian equals tg_lhp.IdThoiGian
            join lhp in _context.LopHocPhans
                on tg_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
            where nguyenvong.IdDangKyDoiLich == id
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
            ).FirstOrDefaultAsync();

        if (nguyen_vong == null)
        {
            return new ApiResponse<NguyenVongThayDoiLichDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy nguyện vọng",
                StatusCode = 404,
            };
        }

        return new ApiResponse<NguyenVongThayDoiLichDto>
        {
            Data = nguyen_vong,
            Status = true,
            Message = "Lấy nguyện vọng thành công",
            StatusCode = 200,
        };
    }

    /**
     * Sua thong tin nguyen vong cua giao vien
     */
    public async Task<ApiResponse<NguyenVongThayDoiLichDto>>
        Update(NguyenVongThayDoiLichDto nguyenVong)
    {
        var nguyenVongUpdate = await _context.DangKyDoiLichs
            .FirstOrDefaultAsync(x => x.IdDangKyDoiLich == nguyenVong.IdDangKyDoiLich);
        if (nguyenVongUpdate == null)
        {
            return new ApiResponse<NguyenVongThayDoiLichDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy nguyện vọng",
                StatusCode = 404,
            };
        }

        // check thoi gian
        if (nguyenVong.ThoiGianBatDauHienTai > nguyenVong.ThoiGianKetThucHienTai)
        {
            return new ApiResponse<NguyenVongThayDoiLichDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian bắt đầu hiện tại phải nhỏ hơn thời gian kết thúc hiện tại",
                StatusCode = 400,
            };
        }
        if (nguyenVong.ThoiGianBatDauMoi > nguyenVong.ThoiGianKetThucMoi)
        {
            return new ApiResponse<NguyenVongThayDoiLichDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian bắt đầu mới phải nhỏ hơn thời gian kết thúc mới",
                StatusCode = 400,
            };
        }
        if (nguyenVong.ThoiGianBatDauHienTai < DateTime.Now)
        {
            return new ApiResponse<NguyenVongThayDoiLichDto>
            {
                Data = null,
                Status = false,
                Message = "Lớp học phần đã bắt đầu không thể thay đổi",
                StatusCode = 400,
            };
        }
        if (nguyenVong.ThoiGianBatDauMoi < DateTime.Now)
        {
            return new ApiResponse<NguyenVongThayDoiLichDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian bắt đầu mới phải lớn hơn thời gian hiện tại",
                StatusCode = 400,
            };
        }

        // check trong khoang cho phep
        var lhp = await _context.LopHocPhans
            .FirstOrDefaultAsync(x => x.IdLopHocPhan == nguyenVong.IdLopHocPhan);
        if (lhp == null)
        {
            return new ApiResponse<NguyenVongThayDoiLichDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy lớp học phần",
                StatusCode = 404,
            };
        }
        if (nguyenVong.ThoiGianBatDauMoi < lhp.ThoiGianBatDau ||
            nguyenVong.ThoiGianKetThucMoi > lhp.ThoiGianKetThuc)
        {
            return new ApiResponse<NguyenVongThayDoiLichDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian mới không nằm trong khoảng thời gian cho phép",
                StatusCode = 400,
            };
        }

        // check trung lich
        var check_trung_lich = await (
            from thoigian in _context.ThoiGians
            join tg_lhp in _context.ThoiGianLopHocPhans
                on thoigian.IdThoiGian equals tg_lhp.IdThoiGian
            where tg_lhp.IdLopHocPhan == nguyenVong.IdLopHocPhan &&
            (
                (thoigian.NgayBatDau <= nguyenVong.ThoiGianBatDauMoi &&
                thoigian.NgayKetThuc >= nguyenVong.ThoiGianBatDauMoi) ||
                (thoigian.NgayBatDau <= nguyenVong.ThoiGianKetThucMoi &&
                thoigian.NgayKetThuc >= nguyenVong.ThoiGianKetThucMoi)
            )
            select thoigian
        ).AnyAsync();
        if (check_trung_lich)
        {
            return new ApiResponse<NguyenVongThayDoiLichDto>
            {
                Data = null,
                Status = false,
                Message = "Trùng lịch với lớp học phần khác",
                StatusCode = 400,
            };
        }

        // update
        nguyenVongUpdate.ThoiGianBatDauMoi = nguyenVong.ThoiGianBatDauMoi;
        nguyenVongUpdate.ThoiGianKetThucMoi = nguyenVong.ThoiGianKetThucMoi;
        nguyenVongUpdate.TrangThai = -1;

        await _context.SaveChangesAsync();

        return new ApiResponse<NguyenVongThayDoiLichDto>
        {
            Data = nguyenVong,
            Status = true,
            Message = "Cập nhật nguyện vọng thành công",
            StatusCode = 200,
        };
    }

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