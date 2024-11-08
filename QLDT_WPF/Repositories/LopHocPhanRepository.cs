using System.Collections.Generic;
using System.Linq;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;

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
        // Query
        var query = await (
            from lhp in _context.LopHocPhans
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs on lhp.IdMonHoc equals mh.IdMonHoc
            select new LopHocPhanDto
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mh.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        return new ApiResponse<List<LopHocPhanDto>>
        {
            Data = query,
            Success = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Lay lop hoc phan by id
     */
    public async Task<ApiResponse<LopHocPhanDto>> GetById(int id)
    {
        // Query
        var query = await (
            from lhp in _context.LopHocPhans
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs on lhp.IdMonHoc equals mh.IdMonHoc
            where lhp.IdLopHocPhan == id
            select new LopHocPhanDto
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mh.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).FirstOrDefaultAsync();

        return new ApiResponse<LopHocPhanDto>
        {
            Data = query,
            Success = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Sua thong tin lop hoc phan
     */
    public async Task<ApiResponse<LopHocPhanDto>> Edit(LopHocPhanDto lopHocPhan)
    {
        // find lhp
        var qr = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == IdLopHocPhan
            select lhp
        ).FirstOrDefaultAsync();

        if (qr == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Success = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        // Update lop hoc phan
        qr.TenHocPhan = lopHocPhan.TenLopHocPhan;
        qr.IdGiaoVien = lopHocPhan.IdGiaoVien;
        qr.IdMonHoc = lopHocPhan.IdMonHoc;
        qr.ThoiGianBatDau = lopHocPhan.ThoiGianBatDau;
        qr.ThoiGianKetThuc = lopHocPhan.ThoiGianKetThuc;

        // Check giao vien, mon hoc
        var mon = await _context.MonHocs.FindAsync(lopHocPhan.IdMonHoc);
        var gv = await _context.GiaoViens.FindAsync(lopHocPhan.IdGiaoVien);
        if (mon == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Success = false,
                Message = "Không tìm thấy môn học"
            };
        }
        if (gv == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Success = false,
                Message = "Không tìm thấy giáo viên"
            };
        }

        // Save
        _context.LopHocPhans.Update(qr);
        await _context.SaveChangesAsync();

        return new ApiResponse<LopHocPhanDto>
        {
            Data = lopHocPhan,
            Success = true,
            Message = "Sửa thông tin lớp học phần thành công"
        };
    }

    /**
     * Them lop hoc phan
     */


    /**
     * Xoa lop hoc phan By Id 
     */

    /** 
     * Get lop hoc phan cua sinh vien tu id
     */
    
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

    
}