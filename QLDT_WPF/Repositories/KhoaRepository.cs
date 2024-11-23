using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;

namespace QLDT_WPF.Repositories;

public class KhoaRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public KhoaRepository()
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
     * Lay tat ca khoa
     */
    public async Task<ApiResponse<List<KhoaDto>>> GetAll()
    {
        var khoa = await (
            from k in _context.Khoas
            select new KhoaDto
            {
                IdKhoa = k.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).ToListAsync();

        return new ApiResponse<List<KhoaDto>>
        {
            Data = khoa,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Lay khoa by id
     */
    public async Task<ApiResponse<KhoaDto>> GetById(string id)
    {
        var khoa = await (
            from kh in _context.Khoas
            where kh.IdKhoa == id
            select new KhoaDto
            {
                IdKhoa = kh.IdKhoa,
                TenKhoa = kh.TenKhoa
            }
        ).FirstOrDefaultAsync();
        return new ApiResponse<KhoaDto>
        {
            Data = khoa,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Sua thong tin khoa
     */
    public async Task<ApiResponse<KhoaDto>> Update(KhoaDto khoaDto)
    {
        var khoa = await _context.Khoas.FirstOrDefaultAsync(k => k.IdKhoa == khoaDto.IdKhoa);
        if (khoa == null)
        {
            return new ApiResponse<KhoaDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy khoa"
            };
        }

        khoa.TenKhoa = khoaDto.TenKhoa;

        _context.Khoas.Update(khoa);
        await _context.SaveChangesAsync();

        return new ApiResponse<KhoaDto>
        {
            Data = khoaDto,
            Status = true,
            Message = "Sửa dữ liệu thành công"
        };
    }

    /**
     * Them khoa
     */
    public async Task<ApiResponse<KhoaDto>> Add(KhoaDto khoa)
    {
        var khoaEntity = new Khoa
        {
            IdKhoa = khoa.IdKhoa,
            TenKhoa = khoa.TenKhoa
        };

        try
        {
            _context.Khoas.Add(khoaEntity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new ApiResponse<KhoaDto>
            {
                Data = null,
                Status = false,
                Message = "Thêm dữ liệu thất bại"
            };
        }

        return new ApiResponse<KhoaDto>
        {
            Data = khoa,
            Status = true,
            Message = "Thêm dữ liệu thành công"
        };
    }

    /**
     * Add Khoa from file cgv
     */
    public async Task<ApiResponse<List<KhoaDto>>> AddListKhoaFromCSV(List<KhoaDto> khoaDtoList)
    {
        // Kiểm tra nếu danh sách null hoặc trống
        if (khoaDtoList == null || !khoaDtoList.Any())
        {
            return new ApiResponse<List<KhoaDto>>
            {
                Status = false,
                Message = "File Không Được Để Trống!",
                Data = null,
            };
        }

        List<KhoaDto> khoaDtoListError = new List<KhoaDto>();
        HashSet<string> processedIds = new HashSet<string>();

        // Kiểm tra các bản ghi trùng lặp trong danh sách CSV
        foreach (var kh in khoaDtoList)
        {
            if (processedIds.Contains(kh.IdKhoa))
            {
                kh.TenKhoa = $"Khoa: {kh.TenKhoa} lỗi trùng ID {kh.IdKhoa} trong file CSV";
                khoaDtoListError.Add(kh);
                continue;
            }

            processedIds.Add(kh.IdKhoa);
        }

        // Loại bỏ các bản ghi trùng lặp khỏi danh sách trước khi kiểm tra với CSDL
        var uniquekhoaDtoList = khoaDtoList.Except(khoaDtoListError).ToList();

        // Kiểm tra với cơ sở dữ liệu và thêm vào danh sách lỗi nếu cần
        foreach (var cch in uniquekhoaDtoList)
        {
            var cct_c = await _context.Khoas
                .FirstOrDefaultAsync(x => x.IdKhoa == cch.IdKhoa);
            if (cct_c != null)
            {
                cch.TenKhoa = $"Khoa: {cch.TenKhoa} đã tồn tại trong CSDL";
                khoaDtoListError.Add(cch);
                continue;
            }
            // Nếu không có lỗi, thêm vào CSDL
            await _context.Khoas.AddAsync(new Khoa
            {
                IdKhoa = cch.IdKhoa,
                TenKhoa = cch.TenKhoa
            });
        }

        // Nếu có bất kỳ lỗi nào trong quá trình xử lý
        if (khoaDtoListError.Any())
        {
            return new ApiResponse<List<KhoaDto>>
            {
                Status = false,
                Message = "Thêm Danh Sách Chương Trình Học Thất Bại! Có lỗi trong danh sách chương trình học.",
                Data = khoaDtoListError,
            };
        }

        // Lưu thay đổi nếu mọi thứ thành công
        await _context.SaveChangesAsync();

        return new ApiResponse<List<KhoaDto>>
        {
            Status = true,
            Message = "Thêm Danh Sách Khoa Thành Công!",
            Data = khoaDtoList,
        };
    }

    /**
     * Xoa khoa By Id 
     */
    public async Task<ApiResponse<KhoaDto>> Delete(string id)
    {
        var khoa = _context.Khoas.FirstOrDefault(k => k.IdKhoa == id);

        if (khoa == null)
        {
            return new ApiResponse<KhoaDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy khoa"
            };
        }

        try
        {
            _context.Khoas.Remove(khoa);
            await _context.SaveChangesAsync();

            return new ApiResponse<KhoaDto>
            {
                Data = null,
                Status = true,
                Message = "Xóa dữ liệu thành công"
            };
        }
        catch (Exception e)
        {
            return new ApiResponse<KhoaDto>
            {
                Data = null,
                Status = false,
                Message = "Xóa dữ liệu thất bại"
            };
        }
        
    }

    /**
     * Lay sinh vien thuoc khoa
     */
    public async Task<ApiResponse<List<SinhVienDto>>> GetSinhVien(string id)
    {
        var khoa = await _context.Khoas.FirstOrDefaultAsync(k => k.IdKhoa == id);
        if (khoa == null)
        {
            return new ApiResponse<List<SinhVienDto>>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy khoa"
            };
        }

        var sinhVien = await (
            from sv in _context.SinhViens
            where sv.IdKhoa == id
            join kh in _context.Khoas
                on sv.IdKhoa equals kh.IdKhoa
            join cth in _context.ChuongTrinhHocs
                on sv.IdChuongTrinhHoc equals cth.IdChuongTrinhHoc
            select new SinhVienDto
            {
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,

                HoTen = sv.HoTen,
                NgaySinh = sv.NgaySinh,
                DiaChi = sv.DiaChi,
                TenKhoa = kh.TenKhoa,
                Lop = sv.Lop,
            }
        ).ToListAsync();

        return new ApiResponse<List<SinhVienDto>>
        {
            Data = sinhVien,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Lay giao vien thuoc khoa
     */
    public async Task<ApiResponse<List<GiaoVienDto>>> GetGiaoVien(string id)
    {
        var khoa = await _context.Khoas.FirstOrDefaultAsync(k => k.IdKhoa == id);

        if (khoa == null)
        {
            return new ApiResponse<List<GiaoVienDto>>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy khoa"
            };
        }

        var giaoVien = await (
            from gv in _context.GiaoViens
            where gv.IdKhoa == id
            join kh in _context.Khoas
                on gv.IdKhoa equals kh.IdKhoa
            select new GiaoVienDto
            {
                IdGiaoVien = gv.IdGiaoVien,
                IdKhoa = gv.IdKhoa,

                TenGiaoVien = gv.TenGiaoVien,
                TenKhoa = kh.TenKhoa,
                Email = gv.Email,
                SoDienThoai = gv.SoDienThoai,
            }
        ).ToListAsync();

        return new ApiResponse<List<GiaoVienDto>>
        {
            Data = giaoVien,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }
}