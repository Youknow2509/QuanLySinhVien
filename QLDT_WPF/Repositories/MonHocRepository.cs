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
     * Them List Mon Hoc From File CSV
     */
    public async Task<ApiResponse<List<MonHocDto>>> AddListMonHocFromCSV(List<MonHocDto> listMonHoc)
    {
        // Kiểm tra nếu danh sách null hoặc trống
        if (listMonHoc == null || !listMonHoc.Any())
        {
            return new ApiResponse<List<MonHocDto>>
            {
                Status = false,
                Message = "File Không Được Để Trống!",
                Data = null,
            };
        }

        List<MonHocDto> listMonHocError = new List<MonHocDto>();
        HashSet<string> processedIds = new HashSet<string>();

        // Kiểm tra các bản ghi trùng lặp trong danh sách CSV
        foreach (var monh in listMonHoc)
        {
            if (processedIds.Contains(monh.IdMonHoc))
            {
                monh.TenMonHoc = $"Môn Học: {monh.TenMonHoc} lỗi trùng ID {monh.IdMonHoc} trong file CSV";
                listMonHocError.Add(monh);
                continue;
            }

            processedIds.Add(monh.IdMonHoc);
        }

        // Loại bỏ các bản ghi trùng lặp khỏi danh sách trước khi kiểm tra với CSDL
        var uniqueListMonHoc = listMonHoc.Except(listMonHocError).ToList();

        // Kiểm tra với cơ sở dữ liệu và thêm vào danh sách lỗi nếu cần
        foreach (var monh in uniqueListMonHoc)
        {
            // Kiểm tra IdMonHoc trong CSDL
            var monhocInDb = await _context.MonHocs
                .FirstOrDefaultAsync(x => x.IdMonHoc == monh.IdMonHoc);
            if (monhocInDb != null)
            {
                monh.TenMonHoc = $"Môn Học: {monh.TenMonHoc} lỗi trùng ID {monh.IdMonHoc}L";
                listMonHocError.Add(monh);
                continue;
            }

            // Kiểm tra IdKhoa trong CSDL
            var khoaInDb = await _context.Khoas
                .FirstOrDefaultAsync(x => x.IdKhoa == monh.IdKhoa);
            if (khoaInDb == null)
            {
                monh.TenMonHoc = $"Môn Học: {monh.TenMonHoc} lỗi không tìm thấy khoa {monh.IdKhoa}";
                listMonHocError.Add(monh);
                continue;
            }

            // Nếu không có lỗi, thêm vào CSDL
            await _context.MonHocs.AddAsync(new MonHoc
            {
                IdMonHoc = monh.IdMonHoc,
                TenMonHoc = monh.TenMonHoc,
                SoTinChi = monh.SoTinChi,
                SoTietHoc = monh.SoTietHoc,
                IdKhoa = monh.IdKhoa
            });
        }

        // Nếu có bất kỳ lỗi nào trong quá trình xử lý
        if (listMonHocError.Any())
        {
            return new ApiResponse<List<MonHocDto>>
            {
                Status = false,
                Message = "Thêm Danh Sách Môn Học Thất Bại! Có lỗi trong danh sách môn học.",
                Data = listMonHocError,
            };
        }

        // Lưu thay đổi nếu mọi thứ thành công
        await _context.SaveChangesAsync();

        return new ApiResponse<List<MonHocDto>>
        {
            Status = true,
            Message = "Thêm Danh Sách Môn Học Thành Công!",
            Data = listMonHoc,
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