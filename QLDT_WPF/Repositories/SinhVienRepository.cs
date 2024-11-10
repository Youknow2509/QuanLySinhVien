using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;
using QLDT_WPF.Services;

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
    public async Task<ApiResponse<List<SinhVienDto>>> GetAll()
    {
        var sinhViens = (
            from sv in _context.SinhViens
            join khoa in _context.Khoas
                on sv.IdKhoa equals khoa.IdKhoa
            join cch in _context.ChuongTrinhHocs
                on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
            select new SinhVienDto
            {
                // List id
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                // list value
                HoTen = sv.HoTen,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh,
                DiaChi = sv.DiaChi,
                TenKhoa = khoa.TenKhoa,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc,
            }
        ).ToList();

        return new ApiResponse<List<SinhVienDto>>
        {
            Success = true,
            Data = sinhViens,
            Message = "Lấy danh sách sinh viên thành công",
        };
    }

    /**
     * Lay sinh vien by id
     */
    public async Task<ApiResponse<SinhVienDto>> GetById(int id)
    {
        var sinhVien = (
            from sv in _context.SinhViens
            join khoa in _context.Khoas
                on sv.IdKhoa equals khoa.IdKhoa
            join cch in _context.ChuongTrinhHocs
                on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
            where sv.IdSinhVien == id
            select new SinhVienDto
            {
                // List id
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                // list value
                HoTen = sv.HoTen,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh,
                DiaChi = sv.DiaChi,
                TenKhoa = khoa.TenKhoa,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc,
            }
        ).FirstOrDefault();

        if (sinhVien == null)
        {
            return new ApiResponse<SinhVienDto>
            {
                Success = false,
                Message = "Không tìm thấy sinh viên",
            };
        }

        return new ApiResponse<SinhVienDto>
        {
            Success = true,
            Data = sinhVien,
            Message = "Lấy sinh viên thành công",
        };
    }

    /**
     * Sua thong tin sinh vien
     */


    /**
     * Xoa Sinh Vien By Id Sinh Vien
     */
    public async Task<ApiResponse<SinhVienDto>> Delete(int id)
    {
        var sinhVien = await _context.SinhViens
            .FirstOrDefaultAsync(x => x.IdSinhVien == id);
        if (sinhVien == null)
        {
            return new ApiResponse<SinhVienDto>
            {
                Success = false,
                Message = "Không tìm thấy sinh viên",
                Data = null,
            };
        }

        _context.SinhViens.Remove(sinhVien);
        await _context.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = "Xóa sinh viên thành công",
            Data = null,
        };
    }

    /**
     * Sinh Vien Thuoc Lop Hoc Phan
     * @param idLopHocPhan
     */
    public async Task<ApiResponse<List<SinhVienDto>>> 
        GetByLopHocPhan(string idLopHocPhan)
    {   
        var lopHocPhan = await _context.LopHocPhans
            .FirstOrDefaultAsync(x => x.IdLopHocPhan == idLopHocPhan);
        if (lopHocPhan == null)
        {
            return new ApiResponse<List<SinhVienDto>>
            {
                Success = false,
                Message = "Không tìm thấy lớp học phần",
            };
        }

        var qr = await (
            from lhp in _context.LopHocPhans
            join svlhp in _context.SinhVienLopHocPhans on lhp.IdLopHocPhan equals svlhp.IdLopHocPhan
            join sv in _context.SinhViens on svlhp.IdSinhVien equals sv.IdSinhVien
            join k in _context.Khoas on sv.IdKhoa equals k.IdKhoa
            join cth in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cth.IdChuongTrinhHoc
            where lhp.IdLopHocPhan == idLopHocPhan
            select new SinhVienDto
            {
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                HoTen = sv.HoTen,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh,
                DiaChi = sv.DiaChi,
                TenKhoa = k.TenKhoa,
                TenChuongTrinhHoc = cth.TenChuongTrinhHoc
            }
        ).ToListAsync();

        return new ApiResponse<List<SinhVienDto>>
        {
            Success = true,
            Data = qr,
            Message = "Lấy danh sách sinh viên thành công",
        };
    }
}