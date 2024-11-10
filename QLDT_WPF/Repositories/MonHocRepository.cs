using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;

namespace QLDT_WPF.Repositories;

public class MonHocRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public MonHocRepository()
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
     * Lay tat ca mon hoc
     */
    public async Task<ApiResponse<List<MonHocDto>>> GetAll()
    {
        var monhocs = await (
            from mh in _context.MonHocs
            join khoa in _context.Khoas on mh.IdKhoa equals khoa.IdKhoa
            select new MonHocDto {
                IdMonHoc = mh.IdMonHoc,
                IdKhoa = khoa.IdKhoa,

                TenMonHoc = mh.TenMonHoc,
                SoTinChi = mh.SoTinChi,
                SoTietHoc = mh.SoTietHoc,
                TenKhoa = khoa.TenKhoa,
            }
        ).ToListAsync();

        return new ApiResponse<List<MonHocDto>> {
            Data = monhocs,
            Status = true,
            Message = "Lấy dữ liệu thành công",
            StatusCode = 200,
        };
    }

    /**
     * Lay mon hoc by id
     */
    public async Task<ApiResponse<MonHocDto>> GetById(string id)
    {
        var monhoc = await (
            from mh in _context.MonHocs
            join khoa in _context.Khoas 
                on mh.IdKhoa equals khoa.IdKhoa
            where mh.IdMonHoc == id
            select new MonHocDto {
                IdMonHoc = mh.IdMonHoc,
                IdKhoa = khoa.IdKhoa,

                TenMonHoc = mh.TenMonHoc,
                SoTinChi = mh.SoTinChi,
                SoTietHoc = mh.SoTietHoc,
                TenKhoa = khoa.TenKhoa,
            }
        ).FirstOrDefaultAsync();

        if (monhoc == null)
        {
            return new ApiResponse<MonHocDto> {
                Data = null,
                Status = false,
                Message = "Không tìm thấy môn học",
                StatusCode = 404,
            };
        }

        return new ApiResponse<MonHocDto> {
            Data = monhoc,
            Status = true,
            Message = "Lấy dữ liệu thành công",
            StatusCode = 200,
        };
    }

    /**
     * Sua thong tin mon hoc
     */
    public async Task<ApiResponse<MonHocDto>> Update(MonHocDto monhocDto)
    {
        var monhocUpgrade = await _context.MonHocs
            .FirstOrDefaultAsync(x => x.IdMonHoc == monhocDto.IdMonHoc);
        if (monhocUpgrade == null)
        {
            return new ApiResponse<MonHocDto> {
                Data = null,
                Status = false,
                Message = "Không tìm thấy môn học",
                StatusCode = 404,
            };
        }

        // check khoa exist
        var khoa = await _context.Khoas
            .FirstOrDefaultAsync(x => x.IdKhoa == monhocDto.IdKhoa);
        if (khoa == null)
        {
            return new ApiResponse<MonHocDto> {
                Data = null,
                Status = false,
                Message = "Không tìm thấy khoa",
                StatusCode = 404,
            };
        }

        monhocUpgrade.TenMonHoc = monhocDto.TenMonHoc;
        monhocUpgrade.SoTinChi = monhocDto.SoTinChi;
        monhocUpgrade.SoTietHoc = monhocDto.SoTietHoc;
        monhocUpgrade.IdKhoa = monhocDto.IdKhoa;

        await _context.SaveChangesAsync();

        return new ApiResponse<MonHocDto> {
            Data = monhocDto,
            Status = true,
            Message = "Cập nhật môn học thành công",
            StatusCode = 200,
        };
    }

    /**
     * Them mon hoc
     */
    public async Task<ApiResponse<MonHocDto>> Add(MonHocDto monhocDto)
    {   
        // check khoa exist
        var khoa = await _context.Khoas
            .FirstOrDefaultAsync(x => x.IdKhoa == monhocDto.IdKhoa);
        if (khoa == null)
        {
            return new ApiResponse<MonHocDto> {
                Data = null,
                Status = false,
                Message = "Không tìm thấy khoa",
                StatusCode = 404,
            };
        }

        if (monhocDto.IdMonHoc == null)
        {
            monhocDto.IdMonHoc = Guid.NewGuid().ToString();
        }

        var monhoc = new MonHoc {
            IdMonHoc = monhocDto.IdMonHoc,
            TenMonHoc = monhocDto.TenMonHoc,
            SoTinChi = monhocDto.SoTinChi,
            SoTietHoc = monhocDto.SoTietHoc,
            IdKhoa = monhocDto.IdKhoa,
        };

        await _context.MonHocs.AddAsync(monhoc);
        await _context.SaveChangesAsync();

        return new ApiResponse<MonHocDto> {
            Data = monhocDto,
            Status = true,
            Message = "Thêm môn học thành công",
            StatusCode = 200,
        };
    }

    /**
     * Xoa mon hoc By Id 
     */
    public async Task<ApiResponse<MonHocDto>> Delete(string id)
    {
        var monhoc = await _context.MonHocs
            .FirstOrDefaultAsync(x => x.IdMonHoc == id);
        if (monhoc == null)
        {
            return new ApiResponse<MonHocDto> {
                Data = null,
                Status = false,
                Message = "Không tìm thấy môn học",
                StatusCode = 404,
            };
        }

        _context.MonHocs.Remove(monhoc);
        await _context.SaveChangesAsync();

        return new ApiResponse<MonHocDto> {
            Data = null,
            Status = true,
            Message = "Xóa môn học thành công",
            StatusCode = 200,
        };
    }

    /**
     * Get data mon hoc for giao vien with id giao vien
     */
    public async Task<ApiResponse<List<MonHocDto>>> GetMonHocForGiaoVien(string idGiaoVien)
    {
        var gv_c = await _context.GiaoViens
            .FirstOrDefaultAsync(x => x.IdGiaoVien == idGiaoVien);
        if (gv_c == null)   
        {
            return new ApiResponse<List<MonHocDto> > {
                Data = null,
                Status = false,
                Message = "Không tìm thấy giáo viên",
                StatusCode = 404,
            };
        }

        var listMH = await (
            from gv in _context.GiaoViens
            where gv.IdGiaoVien == idGiaoVien
            join khoa in _context.Khoas on gv.IdKhoa equals khoa.IdKhoa
            join mh in _context.MonHocs on khoa.IdKhoa equals mh.IdKhoa
            select new MonHocDto{
                IdMonHoc = mh.IdMonHoc,
                TenMonHoc = mh.TenMonHoc,
                SoTinChi = mh.SoTinChi,
                SoTietHoc = mh.SoTietHoc,
                TenKhoa = khoa.TenKhoa,
                IdKhoa = khoa.IdKhoa
            }
        ).ToListAsync();

        return new ApiResponse<List<MonHocDto>> {
            Data = listMH,
            Status = true,
            Message = "Lấy dữ liệu thành công",
            StatusCode = 200,
        };
    }
}