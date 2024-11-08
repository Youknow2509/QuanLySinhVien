using System.Collections.Generic;
using System.Linq;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;

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
        var qr = await (
            from k in _context.Khoas
            join gv in _context.GiaoViens 
                on k.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs
                on k.IdMonHoc equals mh.IdMonHoc
            join sv in _context.SinhViens
                on k.IdSinhVien equals sv.IdSinhVien
            select new KhoaDto
            {
                IdKhoa = k.IdKhoa,
                IdSinhVien = sv.IdSinhVien,
                IdGiaoVien = gv.IdGiaoVien, 
                IdMonHoc = mh.IdMonHoc,

                TenKhoa = k.TenKhoa,
                TenSinhVien = sv.TenSinhVien, 
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
            }
        ).ToListAsync();

        return new ApiResponse<KhoaDto>
        {
            Data = qr,
            Success = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Lay khoa by id
     */
    public async Task<ApiResponse<List<KhoaDto>>> GetById(string id)
    {
        var qr = await (
            from k in _context.Khoas
            where k.IdKhoa == id
            join gv in _context.GiaoViens 
                on k.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs
                on k.IdMonHoc equals mh.IdMonHoc
            join sv in _context.SinhViens
                on k.IdSinhVien equals sv.IdSinhVien
            select new KhoaDto
            {
                IdKhoa = k.IdKhoa,
                IdSinhVien = sv.IdSinhVien,
                IdGiaoVien = gv.IdGiaoVien, 
                IdMonHoc = mh.IdMonHoc,

                TenKhoa = k.TenKhoa,
                TenSinhVien = sv.TenSinhVien, 
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
            }
        ).ToListAsync();

        if (qr == null)
        {
            return new ApiResponse<KhoaDto>
            {
                Data = null,
                Success = false,
                Message = "Không tìm thấy dữ liệu"
            };
        }

        return new ApiResponse<KhoaDto>
        {
            Data = qr,
            Success = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Sua thong tin khoa
     */
    public async Task<ApiResponse<KhoaDto>> Update(KhoaDto khoaDto)
    {
        var khoa = await _context.Khoas.FirstOrDefaultAsync(k => k.IdKhoa == khoaDto.Id);
        if (khoa == null)
        {
            return new ApiResponse<KhoaDto>
            {
                Data = null,
                Success = false,
                Message = "Không tìm thấy khoa"
            };
        }

        khoa.TenKhoa = khoaDto.TenKhoa;
        khoa.IdSinhVien = khoaDto.IdSinhVien;
        khoa.IdGiaoVien = khoaDto.IdGiaoVien;
        khoa.IdMonHoc = khoaDto.IdMonHoc;

        _context.Khoas.Update(khoa);
        await _context.SaveChangesAsync();

        return new ApiResponse<KhoaDto>
        {
            Data = khoaDto,
            Success = true,
            Message = "Sửa dữ liệu thành công"
        };
    }

    /**
     * Them khoa
     */
    public async Task<ApiResponse<KhoaDto>> Add(KhoaDto khoa)
    {
        if (khoa.IdKhoa == null) 
        {
            khoa.IdKhoa = Guid.NewGuid().ToString();
        }

        var newKhoa = new Khoa
        {
            IdKhoa = khoa.IdKhoa,
            TenKhoa = khoa.TenKhoa,
        };

        await _context.Khoas.AddAsync(newKhoa);
        await _context.SaveChangesAsync();

        return new ApiResponse<KhoaDto>
        {
            Data = khoa,
            Success = true,
            Message = "Thêm dữ liệu thành công"
        };
    }

    /**
     * Xoa khoa By Id 
     */

    /**
     * Lay sinh vien thuoc khoa
     */

    /**
     * Lay giao vien thuoc khoa
     */

}