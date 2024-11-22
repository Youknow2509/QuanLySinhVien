using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;

namespace QLDT_WPF.Repositories;

public class DiemRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public DiemRepository()
    {
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
     * Lay tat ca diem
     */
    public async Task<ApiResponse<List<DiemDto>>> GetAll()
    {
        // Query
        var query = await(
            from d in _context.Diems
            join lhp in _context.LopHocPhans
                on d.IdLopHocPhan equals lhp.IdLopHocPhan
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            select new DiemDto
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
                TenLopHocPhan = lhp.TenHocPhan,
            }
        ).ToListAsync();

        // Directly return the JSON result
        return new ApiResponse<List<DiemDto>>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = query
        };
    }

    /**
     * Lay diem by id
     */
    public async Task<ApiResponse<DiemDto>> GetById(string id)
    {
        // Query
        var query = await (
            from d in _context.Diems
            join lhp in _context.LopHocPhans
                on d.IdLopHocPhan equals lhp.IdLopHocPhan
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            where d.IdDiem == id
            select new DiemDto
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
                TenLopHocPhan = lhp.TenHocPhan,
            }
        ).FirstOrDefaultAsync();

        return new ApiResponse<DiemDto>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = query
        };
    }

    /**
     * Lay diem by id sinh vien
     */
    public async Task<ApiResponse<List<DiemDto>>> GetByIdSinhVien(string id)
    {
        // Query
        var query = await(
            from d in _context.Diems
            where d.IdSinhVien == id
            join lhp in _context.LopHocPhans
                on d.IdLopHocPhan equals lhp.IdLopHocPhan
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            join sv in _context.SinhViens
                on d.IdSinhVien equals sv.IdSinhVien
            select new DiemDto
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,
                TenSinhVien = sv.HoTen,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
                TenLopHocPhan = lhp.TenHocPhan,
            }
        ).ToListAsync();

        return new ApiResponse<List<DiemDto>>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = query
        };
    }

    /**
     * Sua thong tin diem
     */
    public async Task<ApiResponse<DiemDto>> Edit(DiemDto diem)
    {
        // Query
        var query = await (
            from d in _context.Diems
            where d.IdDiem == diem.IdDiem
            select d
        ).FirstOrDefaultAsync();

        // Check if the query is null
        if (query == null)
        {
            return new ApiResponse<DiemDto>
            {
                Status = false,
                Message = "Not Found",
                StatusCode = 404,
                Data = null
            };
        }

        // Check diem qua trinh, diem ket thuc, diem tong ket
        if (diem.DiemQuaTrinh < 0 || diem.DiemQuaTrinh > 10)
        {
            return new ApiResponse<DiemDto>
            {
                Status = false,
                Data = null,
                Message = "Điểm quá trình không hợp lệ",
                StatusCode = 400
            };
        }
        if (diem.DiemKetThuc < 0 || diem.DiemKetThuc > 10)
        {
            return new ApiResponse<DiemDto>
            {
                Status = false,
                Data = null,
                Message = "Điểm kết thúc không hợp lệ",
                StatusCode = 400
            };
        }
        if (diem.DiemTongKet < 0 || diem.DiemTongKet > 10)
        {
            return new ApiResponse<DiemDto>
            {
                Status = false,
                Data = null,
                Message = "Điểm tổng kết không hợp lệ",
                StatusCode = 400
            };
        }

        // Check id lop hoc phan, id sinh vien
        var checkLopHocPhan = await _context.LopHocPhans
            .FirstOrDefaultAsync(x => x.IdLopHocPhan == diem.IdLopHocPhan);
        var checkSinhVien = await _context.SinhViens
            .FirstOrDefaultAsync(x => x.IdSinhVien == diem.IdSinhVien);
        if (checkLopHocPhan == null)
        {
            return new ApiResponse<DiemDto>
            {
                Status = false,
                Message = "Id Lop Hoc Phan Not Found",
                StatusCode = 404,
                Data = null
            };
        }
        if (checkSinhVien == null)
        {
            return new ApiResponse<DiemDto>
            {
                Status = false,
                Message = "Id Sinh Vien Not Found",
                StatusCode = 404,
                Data = null
            };
        }

        // Update the query
        query.IdSinhVien = diem.IdSinhVien;
        query.IdLopHocPhan = diem.IdLopHocPhan;
        query.DiemQuaTrinh = diem.DiemQuaTrinh;
        query.DiemKetThuc = diem.DiemKetThuc;
        query.DiemTongKet = diem.DiemTongKet;
        query.LanHoc = diem.LanHoc;

        // Save the changes
        await _context.SaveChangesAsync();

        return new ApiResponse<DiemDto>
        {
            Status = true,
            Message = "Cập nhật điểm thành công",
            StatusCode = 200,
            Data = diem
        };
    }

    /**
     * Them diem
     */
    public async Task<ApiResponse<DiemDto>> Add(DiemDto diemDto)
    {
        // Handle if dont have Id diem
        if (diemDto.IdDiem == null)
        {
            diemDto.IdDiem = Guid.NewGuid().ToString();
        }
        // Check id lop hoc phan, id sinh vien
        var checkLopHocPhan = await _context.LopHocPhans.FindAsync(diemDto.IdLopHocPhan);
        var checkSinhVien = await _context.SinhViens.FindAsync(diemDto.IdSinhVien);
        if (checkLopHocPhan == null)
        {
            return new ApiResponse<DiemDto>
            {
                Status = false,
                Message = "Id Lop Hoc Phan Not Found",
                StatusCode = 404,
                Data = null
            };
        }
        if (checkSinhVien == null)
        {
            return new ApiResponse<DiemDto>
            {
                Status = false,
                Message = "Id Sinh Vien Not Found",
                StatusCode = 404,
                Data = null
            };
        }
        // Create new diem
        var newDiem = new Diem
        {
            IdDiem = diemDto.IdDiem,
            IdLopHocPhan = diemDto.IdLopHocPhan,
            IdSinhVien = diemDto.IdSinhVien,
            DiemQuaTrinh = diemDto.DiemQuaTrinh,
            DiemKetThuc = diemDto.DiemKetThuc,
            DiemTongKet = diemDto.DiemTongKet,
            LanHoc = diemDto.LanHoc,
        };
        // Add new diem
        _context.Diems.Add(newDiem);
        await _context.SaveChangesAsync();

        return new ApiResponse<DiemDto>
        {
            Status = true,
            Message = "Thêm điểm thành công",
            StatusCode = 200,
            Data = diemDto
        };
    }

    /**
     * Xoa diem By Id 
     */
    public async Task<ApiResponse<DiemDto>> Delete(string id)
    {
        // Query
        var query = await (
            from d in _context.Diems
            where d.IdDiem == id
            select d
        ).FirstOrDefaultAsync();

        // Check if the query is null
        if (query == null)
        {
            return new ApiResponse<DiemDto>
            {
                Status = false,
                Message = "Not Found",
                StatusCode = 404,
                Data = null
            };
        }

        // Remove the query
        _context.Diems.Remove(query);
        await _context.SaveChangesAsync();

        return new ApiResponse<DiemDto>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = null
        };
    }

    /**
     * Trả Về Danh Sách Điểm Của Sinh Viên Với Lần Thi Cuối Cùng, 
     * Có Điểm Tổng Kết >= 7, Chưa Có Trong Nguyện Vọng
     */
    public async Task<ApiResponse<List<DiemDto>>> GetDiemDangKyNguyenVongFromSinhVien(string idSinhVien)
    {
        // Lấy các môn học có điểm tổng kết >= 7 và lần học là lớn nhất
        var query = await (
            from diem in _context.Diems
            join lopHocPhan in _context.LopHocPhans
                on diem.IdLopHocPhan equals lopHocPhan.IdLopHocPhan
            join monhoc in _context.MonHocs
                on lopHocPhan.IdMonHoc equals monhoc.IdMonHoc
            where diem.IdSinhVien == idSinhVien && diem.DiemTongKet <= 7
            join latestDiem in (
                from d in _context.Diems
                where d.IdSinhVien == idSinhVien && d.DiemTongKet <= 7
                group d by d.IdLopHocPhan into g
                select new
                {
                    IdLopHocPhan = g.Key,
                    MaxLanHoc = g.Max(x => x.LanHoc)
                }
            ) on new { diem.IdLopHocPhan, diem.LanHoc } equals new { latestDiem.IdLopHocPhan, LanHoc = latestDiem.MaxLanHoc }
            select new DiemDto
            {
                IdDiem = diem.IdDiem,
                IdSinhVien = diem.IdSinhVien,
                IdLopHocPhan = diem.IdLopHocPhan,
                IdMon = monhoc.IdMonHoc,
                TenLopHocPhan = lopHocPhan.TenHocPhan,
                DiemQuaTrinh = diem.DiemQuaTrinh,
                DiemKetThuc = diem.DiemKetThuc,
                DiemTongKet = diem.DiemTongKet,
                LanHoc = diem.LanHoc,
                TenMonHoc = monhoc.TenMonHoc,
            }
        ).ToListAsync();

        // lay cac mon hoc da dang ky cua sinh vien
        var queryDangKy = await (
            from dk in _context.DangKyNguyenVongs
            where dk.IdSinhVien == idSinhVien
            select dk.IdMonHoc
        ).ToListAsync();

        // Loại bỏ các môn học đã đăng ký
        query = query.Where(x => !queryDangKy.Contains(x.IdMon)).ToList();

        return new ApiResponse<List<DiemDto>>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = query
        };
    }

    /**
     * Lay diem by id lop hoc phan
     */
    public async Task<ApiResponse<List<DiemDto>>> GetDiemByIdLopHocPhan(string idLopHocPhan)
    {
        var qr = await (
           from lhp in _context.LopHocPhans
           where lhp.IdLopHocPhan == idLopHocPhan
           join d in _context.Diems
               on lhp.IdLopHocPhan equals d.IdLopHocPhan
           join mon in _context.MonHocs
               on lhp.IdMonHoc equals mon.IdMonHoc
           join sv in _context.SinhViens
               on d.IdSinhVien equals sv.IdSinhVien
           select new DiemDto
           {
               IdDiem = d.IdDiem,
               IdSinhVien = d.IdSinhVien,
               IdLopHocPhan = d.IdLopHocPhan,
               IdMon = mon.IdMonHoc,

               TenSinhVien = sv.HoTen,
               DiemQuaTrinh = d.DiemQuaTrinh,
               DiemKetThuc = d.DiemKetThuc,
               DiemTongKet = d.DiemTongKet,
               LanHoc = d.LanHoc,
               TenMonHoc = mon.TenMonHoc,
               TenLopHocPhan = lhp.TenHocPhan,
           }
       ).ToListAsync();

        return new ApiResponse<List<DiemDto>>
        {
            Status = true,
            Message = "Success",
            StatusCode = 200,
            Data = qr
        };
    }

    /**
     * Nhập điểm cho sinh viên
     */
    public async Task<ApiResponse<NhapDiemDto>> NhapDiemSinhVien(NhapDiemDto nhapDiemDto)
    {
        if (nhapDiemDto.IdDiem == null)
        {
            return new ApiResponse<NhapDiemDto>
            {
                Status = false,
                Message = "Id Diem is required",
                StatusCode = 400,
                Data = null
            };
        }
        // Query
        var qr = _context.Diems.Where(x => x.IdDiem == nhapDiemDto.IdDiem).FirstOrDefault();
        if (qr == null)
        {
            return new ApiResponse<NhapDiemDto>
            {
                Status = false,
                Message = "Id Diem Not Found",
                StatusCode = 404,
                Data = null
            };
        }
        qr.DiemQuaTrinh = nhapDiemDto.DiemQuaTrinh;
        qr.DiemKetThuc = nhapDiemDto.DiemKetThuc;
        qr.DiemTongKet = nhapDiemDto.DiemTongKet;

        _context.Diems.Update(qr);
        await _context.SaveChangesAsync();

        return new ApiResponse<NhapDiemDto>
        {
            Status = true,
            Message = "Nhập điểm thành công",
            StatusCode = 200,
            Data = nhapDiemDto
        };
    }

    /**
     * Nhap 'List' Diem Sinh Vien
     */
    public async Task<ApiResponse<List<NhapDiemDto>>> NhapDiemSinhVienList(List<NhapDiemDto> listDiem)
    {
        foreach (NhapDiemDto diem in listDiem)
        {
            if (diem.IdDiem == null)
            {
                return new ApiResponse<List<NhapDiemDto>>
                {
                    Status = false,
                    Message = "Id Diem is required",
                    StatusCode = 400,
                    Data = null
                };
            }
            // Query
            var qr = _context.Diems.Where(x => x.IdDiem == diem.IdDiem).FirstOrDefault();
            if (qr == null)
            {
                return new ApiResponse<List<NhapDiemDto>>
                {
                    Status = false,
                    Message = "Id Diem Not Found",
                    StatusCode = 404,
                    Data = null
                };
            }
            qr.DiemQuaTrinh = diem.DiemQuaTrinh;
            qr.DiemKetThuc = diem.DiemKetThuc;
            qr.DiemTongKet = diem.DiemTongKet;
            _context.Diems.Update(qr);
        }
        await _context.SaveChangesAsync();

        return new ApiResponse<List<NhapDiemDto>>
        {
            Status = true,
            Message = "Nhập điểm thành công",
            StatusCode = 200,
            Data = listDiem
        };
    }

}