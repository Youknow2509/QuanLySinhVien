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
    public async Task<ApiResponse<LopHocPhanDto>> Add(LopHocPhanDto lopHocPhan)
    {
        // if id lop hoc phan is null -> generate new id
        if (lopHocPhan.IdLopHocPhan == null)
        {
            lopHocPhan.IdLopHocPhan = Guid.NewGuid().ToString();
        }

        // Check thoi gian
        if (lopHocPhan.ThoiGianBatDau >= lopHocPhan.ThoiGianKetThuc)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Success = false,
                Message = "Thời gian kết thúc phải lớn hơn thời gian bắt đầu"
            };
        }

        // Xử lí lớp học phần tạo chỉ được tạo ở tương lai
        if (lopHocPhan.ThoiGianBatDau < DateTime.Now
            || lopHocPhan.ThoiGianKetThuc < DateTime.Now)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Success = false,
                Message = "Thời gian bắt đầu và kết thúc phải lớn hơn thời gian hiện tại"
            };
        }

        // Create new lop hoc phan
        var newLopHocPhan = new LopHocPhan
        {
            IdLopHocPhan = lopHocPhan.IdLopHocPhan,
            TenHocPhan = lopHocPhan.TenLopHocPhan,
            IdGiaoVien = lopHocPhan.IdGiaoVien,
            IdMonHoc = lopHocPhan.IdMonHoc,
            ThoiGianBatDau = lopHocPhan.ThoiGianBatDau,
            ThoiGianKetThuc = lopHocPhan.ThoiGianKetThuc,
        };

        // Check giao vien, mon hoc
        var mon = await _context.MonHocs.FindAsync(lopHocPhan.IdMonHoc);
        if (mon == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Success = false,
                Message = "Không tìm thấy môn học"
            };
        }
        var gv = await _context.GiaoViens.FindAsync(lopHocPhan.IdGiaoVien);
        if (gv == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Success = false,
                Message = "Không tìm thấy giáo viên"
            };
        }

        // Add new lop hoc phan
        _context.LopHocPhans.Add(newLopHocPhan);
        _context.SaveChanges();

        return new ApiResponse<LopHocPhanDto>
        {
            Data = lopHocPhan,
            Success = true,
            Message = "Thêm lớp học phần thành công"
        };
    }

    /**
     * Xoa lop hoc phan By Id 
     */
    public async Task<ApiResponse<LopHocPhanDto>> Delete(string id)
    {
        // Find lop hoc phan
        var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
        if (lopHocPhan == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Success = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        // Remove lop hoc phan
        _context.LopHocPhans.Remove(lopHocPhan);
        await _context.SaveChangesAsync();

        return new ApiResponse<LopHocPhanDto>
        {
            Data = null,
            Success = true,
            Message = "Xóa lớp học phần thành công"
        };
    }

    /** 
     * Get lop hoc phan cua sinh vien tu id
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>> GetLopHocPhansFromSinhVien(string id)
    {
        // check sv exist
        var sv_c = await _context.SinhViens.FirstOrDefaultAsync(x => x.IdSinhVien == id);
        if (sv_c == null)
        {
            return new ApiResponse<List<LopHocPhanDto>>
            {
                Data = null,
                Success = false,
                Message = "Không tìm thấy sinh viên"
            };
        }
        // Query
        var query = await (
            from sv in sv_c
            join svlhp in _context.SinhVienLopHocPhans
                on sv.IdSinhVien equals svlhp.IdSinhVien
            join lhp in _context.LopHocPhans
                on svlhp.IdLopHocPhan equals lhp.IdLopHocPhan
            join gv in _context.GiaoViens
                on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            select new LopHocPhanDto
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mon.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mon.IdMonHoc,
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
     * Get lop hoc phan cua giao vien tu id
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>> GetLopHocPhansFromGiaoVien(string id)
    {
        var gv_c = await _context.GiaoViens
            .FirstOrDefaultAsync(x => x.IdGiaoVien == id);

        if (gv_c == null)
        {
            return new ApiResponse<List<LopHocPhanDto>>
            {
                Data = null,
                Status = false,
                Message = "Không Tìm Thấy Giáo Viên",
            };
        }

        vả qr = await (
            from gv in gv_c
            join lhp in _context.LopHocPhans
                on gv.IdGiaoVien equals lhp.IdGiaoVien
            join mon in _context.MonHocs
                on mon.IdLopHocPhan equals lhp.IdLopHocPhan
            select new LopHocPhanDto
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mon.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mon.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        return new ApiResponse<List<LopHocPhanDto>>{
            Data = query,
            Success = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

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