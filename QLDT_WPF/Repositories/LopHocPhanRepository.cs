using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using Azure.Identity;
using QLDT_WPF.Models;
using QLDT_WPF.Views.Shared.Components.Admin.Help;

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
        var lhp = await (
            from l in _context.LopHocPhans
            join gv in _context.GiaoViens
                on l.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs
                on l.IdMonHoc equals mh.IdMonHoc
            select new LopHocPhanDto
            {
                IdLopHocPhan = l.IdLopHocPhan,
                IdMonHoc = l.IdMonHoc,
                IdGiaoVien = l.IdGiaoVien,

                TenLopHocPhan = l.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                ThoiGianBatDau = l.ThoiGianBatDau,
                ThoiGianKetThuc = l.ThoiGianKetThuc,
                TrangThaiNhapDiem = l.TrangThaiNhapDiem,
            }
        ).ToListAsync();

        return new ApiResponse<List<LopHocPhanDto>>
        {
            Data = lhp,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Lay lop hoc phan by id
     */
    public async Task<ApiResponse<LopHocPhanDto>> GetById(string id)
    {
        var LopHocPhan = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == id
            join gv in _context.GiaoViens
                on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs
                on lhp.IdMonHoc equals mh.IdMonHoc
            select new LopHocPhanDto
            {
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdMonHoc = lhp.IdMonHoc,
                IdGiaoVien = lhp.IdGiaoVien,

                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc,
                TrangThaiNhapDiem = lhp.TrangThaiNhapDiem,
            }
        ).FirstOrDefaultAsync();

        return new ApiResponse<LopHocPhanDto>
        {
            Data = LopHocPhan,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Sua thong tin lop hoc phan
     */
    public async Task<ApiResponse<LopHocPhanDto>> Edit(LopHocPhanDto lopHocPhan)
    {
        var lhp = await _context.LopHocPhans
            .FirstOrDefaultAsync(l => l.IdLopHocPhan == lopHocPhan.IdLopHocPhan);
        if (lhp == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        // check giao vien, mon hoc ton tai
        var gv = await _context.GiaoViens
            .FirstOrDefaultAsync(g => g.IdGiaoVien == lopHocPhan.IdGiaoVien);
        if (gv == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy giáo viên"
            };
        }
        var mh = await _context.MonHocs
            .FirstOrDefaultAsync(m => m.IdMonHoc == lopHocPhan.IdMonHoc);
        if (mh == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy môn học"
            };
        }

        // check thoi gian thay doi
        if ((lhp.ThoiGianBatDau != lopHocPhan.ThoiGianBatDau
            || lhp.ThoiGianKetThuc != lopHocPhan.ThoiGianKetThuc)
            && lhp.ThoiGianBatDau >= DateTime.Now)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không thể thay đổi thời gian lớp học phần khi lớp học phần đã diễn ra"
            };
        }

        // update lop hoc phan
        lhp.TenHocPhan = lopHocPhan.TenLopHocPhan;
        lhp.IdGiaoVien = lopHocPhan.IdGiaoVien;
        lhp.IdMonHoc = lopHocPhan.IdMonHoc;
        lhp.ThoiGianBatDau = lopHocPhan.ThoiGianBatDau;
        lhp.ThoiGianKetThuc = lopHocPhan.ThoiGianKetThuc;

        await _context.SaveChangesAsync();

        return new ApiResponse<LopHocPhanDto>
        {
            Data = lopHocPhan,
            Status = true,
            Message = "Sửa thông tin lớp học phần thành công"
        };
    }

    /**
     * Them lop hoc phan
     */
    public async Task<ApiResponse<LopHocPhanDto>> Add(LopHocPhanDto lopHocPhan)
    {
        if (lopHocPhan.IdLopHocPhan == null)
        {
            lopHocPhan.IdLopHocPhan = Guid.NewGuid().ToString();
        }

        LopHocPhan newLopHocPhan = new LopHocPhan
        {
            IdLopHocPhan = lopHocPhan.IdLopHocPhan,
            IdMonHoc = lopHocPhan.IdMonHoc,
            IdGiaoVien = lopHocPhan.IdGiaoVien,
            TenHocPhan = lopHocPhan.TenLopHocPhan,
            ThoiGianBatDau = lopHocPhan.ThoiGianBatDau,
            ThoiGianKetThuc = lopHocPhan.ThoiGianKetThuc,
            TrangThaiNhapDiem = lopHocPhan.TrangThaiNhapDiem
        };

        // check giao vien, mon hoc ton tai
        var gv = await _context.GiaoViens
            .FirstOrDefaultAsync(g => g.IdGiaoVien == lopHocPhan.IdGiaoVien);
        if (gv == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy giáo viên"
            };
        }
        var mh = await _context.MonHocs
            .FirstOrDefaultAsync(m => m.IdMonHoc == lopHocPhan.IdMonHoc);
        if (mh == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy môn học"
            };
        }

        // check thoi gian
        if (newLopHocPhan.ThoiGianBatDau <= DateTime.Now || newLopHocPhan.ThoiGianKetThuc <= DateTime.Now)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không thể thêm lớp học phần khi thời gian lớp học phần đã diễn ra"
            };
        }
        if (newLopHocPhan.ThoiGianBatDau >= newLopHocPhan.ThoiGianKetThuc)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian bắt đầu phải trước thời gian kết thúc"
            };
        }

        // handle add lop hoc phan
        await _context.LopHocPhans.AddAsync(newLopHocPhan);
        await _context.SaveChangesAsync();

        return
            new ApiResponse<LopHocPhanDto>
            {
                Data = lopHocPhan,
                Status = true,
                Message = "Thêm lớp học phần thành công"
            };
    }

    /**
     * Add lop hoc phan with file 
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>> AddListLopHocPhanFromCSV(List<LopHocPhanDto> listLopHocPhan)
    {
        // Kiểm tra nếu danh sách null hoặc trống
        if (listLopHocPhan == null || !listLopHocPhan.Any())
        {
            return new ApiResponse<List<LopHocPhanDto>>
            {
                Status = false,
                Message = "File Không Được Để Trống!",
                Data = null,
            };
        }

        List<LopHocPhanDto> listLopHocPhanError = new List<LopHocPhanDto>();
        HashSet<string> processedIds = new HashSet<string>();

        // Kiểm tra các bản ghi trùng lặp trong danh sách CSV
        foreach (var lhp in listLopHocPhan)
        {
            if (processedIds.Contains(lhp.IdLopHocPhan))
            {
                lhp.TenLopHocPhan = $"Lớp Học Phần: {lhp.TenLopHocPhan} lỗi trùng ID {lhp.IdLopHocPhan} trong file CSV";
                listLopHocPhanError.Add(lhp);
                continue;
            }

            processedIds.Add(lhp.IdLopHocPhan);
        }

        // Loại bỏ các bản ghi trùng lặp khỏi danh sách trước khi kiểm tra với CSDL
        var uniquelistLopHocPhan = listLopHocPhan.Except(listLopHocPhanError).ToList();

        // Kiểm tra với cơ sở dữ liệu và thêm vào danh sách lỗi nếu cần
        foreach (var lhp in uniquelistLopHocPhan)
        {
            // Kiểm tra id lop hoc phan trong CSDL
            var lhp_c = await _context.LopHocPhans
                .FirstOrDefaultAsync(l => l.IdLopHocPhan == lhp.IdLopHocPhan);
            if (lhp_c != null)
            {
                lhp.TenLopHocPhan = $"Lớp Học Phần: {lhp.TenLopHocPhan} lỗi trùng ID {lhp.IdLopHocPhan} trong CSDL";
                listLopHocPhanError.Add(lhp);
                continue;
            }

            // Kiểm tra giáo viên, môn học trong CSDL
            var gv = await _context.GiaoViens
                .FirstOrDefaultAsync(g => g.IdGiaoVien == lhp.IdGiaoVien);
            if (gv == null)
            {
                lhp.TenLopHocPhan = $"Lớp Học Phần: {lhp.TenLopHocPhan} lỗi không tìm thấy giáo viên {lhp.IdGiaoVien}";
                listLopHocPhanError.Add(lhp);
                continue;
            }

            var mh = await _context.MonHocs
                .FirstOrDefaultAsync(m => m.IdMonHoc == lhp.IdMonHoc);
            if (mh == null)
            {
                lhp.TenLopHocPhan = $"Lớp Học Phần: {lhp.TenLopHocPhan} lỗi không tìm thấy môn học {lhp.IdMonHoc}";
                listLopHocPhanError.Add(lhp);
                continue;
            }

            // Check thoi gian
            if (lhp.ThoiGianBatDau <= DateTime.Now || lhp.ThoiGianKetThuc <= DateTime.Now)
            {
                lhp.TenLopHocPhan = $"Lớp Học Phần: {lhp.TenLopHocPhan} lỗi thời gian lớp học phần đã diễn ra";
                listLopHocPhanError.Add(lhp);
                continue;
            }
            if (lhp.ThoiGianBatDau >= lhp.ThoiGianKetThuc)
            {
                lhp.TenLopHocPhan = $"Lớp Học Phần: {lhp.TenLopHocPhan} lỗi thời gian bắt đầu phải trước thời gian kết thúc";
                listLopHocPhanError.Add(lhp);
                continue;
            }

            await _context.LopHocPhans.AddAsync(new LopHocPhan
            {
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdMonHoc = lhp.IdMonHoc,
                IdGiaoVien = lhp.IdGiaoVien,
                TenHocPhan = lhp.TenLopHocPhan,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc,
                TrangThaiNhapDiem = lhp.TrangThaiNhapDiem
            });
        }

        // Nếu có bất kỳ lỗi nào trong quá trình xử lý
        if (listLopHocPhanError.Any())
        {
            return new ApiResponse<List<LopHocPhanDto>>
            {
                Status = false,
                Message = "Thêm Danh Sách Lớp Học Phần Thất Bại! Có lỗi trong danh sách Lớp Học Phần.",
                Data = listLopHocPhanError,
            };
        }

        // Lưu thay đổi nếu mọi thứ thành công
        await _context.SaveChangesAsync();

        return new ApiResponse<List<LopHocPhanDto>>
        {
            Status = true,
            Message = "Thêm Danh Sách Lớp Học Phần Thành Công!",
            Data = listLopHocPhan,
        };
    }

    /**
     * Xoa lop hoc phan By Id 
     */
    public async Task<ApiResponse<LopHocPhanDto>> Delete(string id)
    {
        var lhp = _context.LopHocPhans
            .FirstOrDefault(l => l.IdLopHocPhan == id);
        if (lhp == null)
        {
            return new ApiResponse<LopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        _context.Remove(lhp);
        await _context.SaveChangesAsync();

        return new ApiResponse<LopHocPhanDto>
        {
            Data = null,
            Status = true,
            Message = "Xóa lớp học phần thành công"
        };
    }

    /** 
     * Get lop hoc phan cua sinh vien tu id
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>> GetLopHocPhansFromSinhVien(string id)
    {
        var sinhvien = await _context.SinhViens.
            FirstOrDefaultAsync(s => s.IdSinhVien == id);
        if (sinhvien == null)
        {
            return new ApiResponse<List<LopHocPhanDto>>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy sinh viên"
            };
        }

        var list_lhp = await (
            from sv_lhp in _context.SinhVienLopHocPhans
            where sv_lhp.IdSinhVien == id
            join lhp in _context.LopHocPhans
                on sv_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
            join gv in _context.GiaoViens
                on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs
                on lhp.IdMonHoc equals mh.IdMonHoc
            select new LopHocPhanDto
            {
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdMonHoc = lhp.IdMonHoc,
                IdGiaoVien = lhp.IdGiaoVien,

                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc,
                TrangThaiNhapDiem = lhp.TrangThaiNhapDiem
            }
        ).ToListAsync();

        return new ApiResponse<List<LopHocPhanDto>>
        {
            Data = list_lhp,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /** 
     * Get lop hoc phan cua giao vien tu id
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>>
        GetLopHocPhansFromGiaoVien(string id)
    {
        var giaovien = await _context.GiaoViens.
            FirstOrDefaultAsync(g => g.IdGiaoVien == id);
        if (giaovien == null)
        {
            return new ApiResponse<List<LopHocPhanDto>>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy giáo viên"
            };
        }

        var list_lhp = await (
            from lhp in _context.LopHocPhans
            where lhp.IdGiaoVien == id
            join gv in _context.GiaoViens
                on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs
                on lhp.IdMonHoc equals mh.IdMonHoc
            select new LopHocPhanDto
            {
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdMonHoc = lhp.IdMonHoc,
                IdGiaoVien = lhp.IdGiaoVien,

                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc,
                TrangThaiNhapDiem = lhp.TrangThaiNhapDiem,
            }
        ).ToListAsync();

        return new ApiResponse<List<LopHocPhanDto>>
        {
            Data = list_lhp,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Get lop hoc phan tu id mon hoc
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>>
        GetLopHocPhansFromMonHoc(string id)
    {
        var monhoc = await _context.MonHocs
            .FirstOrDefaultAsync(x => x.IdMonHoc == id);
        if (monhoc == null)
        {
            return new ApiResponse<List<LopHocPhanDto>>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy môn học"
            };
        }

        var list_lhp = await (
            from lhp in _context.LopHocPhans
            where lhp.IdMonHoc == id
            join gv in _context.GiaoViens
                on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs
                on lhp.IdMonHoc equals mh.IdMonHoc
            select new LopHocPhanDto
            {
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdMonHoc = lhp.IdMonHoc,
                IdGiaoVien = lhp.IdGiaoVien,

                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc,
                TrangThaiNhapDiem = lhp.TrangThaiNhapDiem,
            }
        ).ToListAsync();

        return new ApiResponse<List<LopHocPhanDto>>
        {
            Data = list_lhp,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Thay doi thoi gian lop hoc phan 
     */
    public async Task<ApiResponse<ThayDoiThoiGianLopHocPhanDto>>
        ChangeTime(ThayDoiThoiGianLopHocPhanDto thayDoiThoiGianLopHocPhan)
    {
        var thoiGian = await _context.ThoiGians
            .FirstOrDefaultAsync(t => t.IdThoiGian == thayDoiThoiGianLopHocPhan.IdThoiGian);
        if (thoiGian == null)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy thời gian"
            };
        }

        var lopHocPhan = await _context.LopHocPhans
            .FirstOrDefaultAsync(l => l.IdLopHocPhan == thayDoiThoiGianLopHocPhan.IdLopHocPhan);
        if (lopHocPhan == null)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        // Check thoi gian co thoa man khong
        if (thayDoiThoiGianLopHocPhan.ThoiGianBatDau >= thayDoiThoiGianLopHocPhan.ThoiGianKetThuc)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian bắt đầu phải trước thời gian kết thúc"
            };
        }

        // Check Thời Gian Truyền Vào Ở Quá Khứ
        if (thayDoiThoiGianLopHocPhan.ThoiGianBatDau <= DateTime.Now
            || thayDoiThoiGianLopHocPhan.ThoiGianKetThuc <= DateTime.Now)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không thể thay đổi thời gian lớp học phần khi thời gian lớp học phần đã diễn ra"
            };
        }

        // Check trong khoang cho phep
        if (thayDoiThoiGianLopHocPhan.ThoiGianBatDau < lopHocPhan.ThoiGianBatDau
            || thayDoiThoiGianLopHocPhan.ThoiGianKetThuc > lopHocPhan.ThoiGianKetThuc)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian thay đổi không nằm trong khoảng thời gian cho phép"
            };
        }

        // check thoi gian lop hoc phan co bi trung khong 
        var check_trung_thoi_gian_lop_hoc_phan = await (
            from tg in _context.ThoiGians
            join tg_lhp in _context.ThoiGianLopHocPhans
                on tg.IdThoiGian equals tg_lhp.IdThoiGian
            where tg_lhp.IdLopHocPhan == thayDoiThoiGianLopHocPhan.IdLopHocPhan
                && (
                    (thayDoiThoiGianLopHocPhan.ThoiGianBatDau >= tg.NgayBatDau
                        && thayDoiThoiGianLopHocPhan.ThoiGianBatDau <= tg.NgayKetThuc)
                    || (thayDoiThoiGianLopHocPhan.ThoiGianKetThuc >= tg.NgayBatDau
                        && thayDoiThoiGianLopHocPhan.ThoiGianKetThuc <= tg.NgayKetThuc)
                )
            select tg
        ).AnyAsync();
        if (check_trung_thoi_gian_lop_hoc_phan)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian lớp học phần bị trùng"
            };
        }

        // update thoi gian
        thoiGian.NgayBatDau = thayDoiThoiGianLopHocPhan.ThoiGianBatDau;
        thoiGian.NgayKetThuc = thayDoiThoiGianLopHocPhan.ThoiGianKetThuc;
        thoiGian.DiaDiem = thayDoiThoiGianLopHocPhan.DiaDiem;

        _context.ThoiGians.Update(thoiGian);
        await _context.SaveChangesAsync();

        return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
        {
            Data = thayDoiThoiGianLopHocPhan,
            Status = true,
            Message = "Thay đổi thời gian lớp học phần thành công"
        };
    }

    /**
     * Thêm thời gian cho lớp học phần
     */
    public async Task<ApiResponse<ThayDoiThoiGianLopHocPhanDto>>
        AddThoiGian(ThayDoiThoiGianLopHocPhanDto thoiGianLopHocPhan)
    {
        var mon = await (
            from lh in _context.LopHocPhans
            join mh in _context.MonHocs
                on lh.IdMonHoc equals mh.IdMonHoc
            where lh.IdLopHocPhan == thoiGianLopHocPhan.IdLopHocPhan
            select mh
        ).FirstOrDefaultAsync();

        if (mon == null)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy môn học"
            };
        }

        // Check da du tiet chua
        var tg_lhp = await (
            from tg in _context.ThoiGians
            join tglhp in _context.ThoiGianLopHocPhans
                on tg.IdThoiGian equals tglhp.IdThoiGian
            where tglhp.IdLopHocPhan == thoiGianLopHocPhan.IdLopHocPhan
            select tg
        ).ToListAsync();
        if (tg_lhp.Count() * 3 >= mon.SoTietHoc)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Đã đủ số tiết học không thể thêm thời gian"
            };
        }

        // Check thoi gian co thoa man khong
        if (thoiGianLopHocPhan.ThoiGianBatDau >= thoiGianLopHocPhan.ThoiGianKetThuc)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian bắt đầu phải trước thời gian kết thúc"
            };
        }

        // Check Thời Gian Truyền Vào Ở Quá Khứ
        if (thoiGianLopHocPhan.ThoiGianBatDau <= DateTime.Now
            || thoiGianLopHocPhan.ThoiGianKetThuc <= DateTime.Now)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Không thể thay đổi thời gian lớp học phần khi thời gian lớp học phần đã diễn ra"
            };
        }

        // Check trong khoang cho phep
        var lhp = await _context.LopHocPhans
            .FirstOrDefaultAsync(x => x.IdLopHocPhan == thoiGianLopHocPhan.IdLopHocPhan);
        if (thoiGianLopHocPhan.ThoiGianBatDau < lhp.ThoiGianBatDau
            || thoiGianLopHocPhan.ThoiGianKetThuc > lhp.ThoiGianKetThuc)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian thay đổi không nằm trong khoảng thời gian cho phép"
            };
        }

        // check thoi gian lop hoc phan co bi trung khong 
        var check_trung_thoi_gian_lop_hoc_phan = await (
            from tg in _context.ThoiGians
            join tglhp in _context.ThoiGianLopHocPhans
                on tg.IdThoiGian equals tglhp.IdThoiGian
            where tglhp.IdLopHocPhan == thoiGianLopHocPhan.IdLopHocPhan
                && (
                    (thoiGianLopHocPhan.ThoiGianBatDau >= tg.NgayBatDau
                        && thoiGianLopHocPhan.ThoiGianBatDau <= tg.NgayKetThuc)
                    || (thoiGianLopHocPhan.ThoiGianKetThuc >= tg.NgayBatDau
                        && thoiGianLopHocPhan.ThoiGianKetThuc <= tg.NgayKetThuc)
                )
            select tg
        ).AnyAsync();
        if (check_trung_thoi_gian_lop_hoc_phan)
        {
            return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
            {
                Data = null,
                Status = false,
                Message = "Thời gian lớp học phần bị trùng"
            };
        }

        // add thoi gian
        ThoiGian newThoiGian = new ThoiGian
        {
            IdThoiGian = Guid.NewGuid().ToString(),
            NgayBatDau = thoiGianLopHocPhan.ThoiGianBatDau,
            NgayKetThuc = thoiGianLopHocPhan.ThoiGianKetThuc,
            DiaDiem = thoiGianLopHocPhan.DiaDiem
        };

        await _context.ThoiGians.AddAsync(newThoiGian);
        await _context.SaveChangesAsync();

        ThoiGianLopHocPhan newThoiGianLopHocPhan = new ThoiGianLopHocPhan
        {
            IdThoiGian = newThoiGian.IdThoiGian,
            IdLopHocPhan = thoiGianLopHocPhan.IdLopHocPhan
        };

        await _context.ThoiGianLopHocPhans.AddAsync(newThoiGianLopHocPhan);
        await _context.SaveChangesAsync();

        return new ApiResponse<ThayDoiThoiGianLopHocPhanDto>
        {
            Data = thoiGianLopHocPhan,
            Status = true,
            Message = "Thêm thời gian lớp học phần thành công"
        };
    }

    /**
     * Handle Add List Thoi Gian Lop Hoc Phan - Loi Hien Thi O Dia Diem
     */
    public async Task<ApiResponse<List<ThayDoiThoiGianLopHocPhanDto>>>
        AddListThoiGianLopHocPhan(List<ThayDoiThoiGianLopHocPhanDto> listThoiGianLopHocPhan)
    {
        // check xem lop hoc phan du tiet chua
        List<ThayDoiThoiGianLopHocPhanDto> listThoiGianLopHocPhanError = new List<ThayDoiThoiGianLopHocPhanDto>();

        foreach (var item in listThoiGianLopHocPhan)
        {
            var req_add = await AddThoiGian(item);
            if (req_add.Status == false)
            {
                item.DiaDiem = $"Thời Gian: {item.DiaDiem} lỗi {req_add.Message}";
                listThoiGianLopHocPhanError.Add(item);
            }
        }

        // Nếu có bất kỳ lỗi nào trong quá trình xử lý
        if (listThoiGianLopHocPhanError.Any())
        {
            return new ApiResponse<List<ThayDoiThoiGianLopHocPhanDto>>
            {
                Status = false,
                Message = "Thêm Danh Sách Thời Gian Lớp Học Phần Thất Bại! Có lỗi trong danh sách Thời Gian Lớp Học Phần.",
                Data = listThoiGianLopHocPhanError,
            };
        }
        return new ApiResponse<List<ThayDoiThoiGianLopHocPhanDto>>
        {
            Status = true,
            Message = "Thêm Danh Sách Thời Gian Lớp Học Phần Thành Công!",
            Data = listThoiGianLopHocPhan,
        };
    }

    /**
     * Kiểm Tra Xem Tồn Tại Lớp Học Phần Của Giáo Viên Hiện Tại Và Tương Lai Không 
     */
    public async Task<ApiResponse<bool>> CheckLopHocPhanGiaoVien(string idGiaoVien)
    {
        // Check Giao Vien Ton Tai Khong
        var giaovien = await _context.GiaoViens
            .FirstOrDefaultAsync(g => g.IdGiaoVien == idGiaoVien);
        if (giaovien == null)
        {
            return new ApiResponse<bool>
            {
                Data = false,
                Status = false,
                Message = "Không tìm thấy giáo viên"
            };
        }

        var lopHocPhan = await (
            from lhp in _context.LopHocPhans
            where lhp.IdGiaoVien == idGiaoVien &&
                (lhp.ThoiGianBatDau >= DateTime.Now || lhp.ThoiGianKetThuc >= DateTime.Now)
            select lhp
        ).AnyAsync();

        return new ApiResponse<bool>
        {
            Data = lopHocPhan,
            Status = true,
            Message = "Kiểm tra thành công"
        };
    }

    /**
     * Tat Nhap Diem Lop Hoc Phan
     */
    public async Task<ApiResponse<bool>> TatNhapDiem(string id)
    {
        // Check Lop Hoc Phan Ton Tai Khong
        var lhp = await _context.LopHocPhans
            .FirstOrDefaultAsync(l => l.IdLopHocPhan == id);
        if (lhp == null)
        {
            return new ApiResponse<bool>
            {
                Data = false,
                Status = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        lhp.TrangThaiNhapDiem = false;

        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Data = true,
            Status = true,
            Message = "Tắt nhập điểm thành công"
        };
    }

    /**
     * Bat Trang Thai Nhap Diem Lop Hoc Phan
     */
    public async Task<ApiResponse<bool>> BatNhapDiem(string id)
    {
        // Check Lop Hoc Phan Ton Tai Khong
        var lhp = await _context.LopHocPhans
            .FirstOrDefaultAsync(l => l.IdLopHocPhan == id);
        if (lhp == null)
        {
            return new ApiResponse<bool>
            {
                Data = false,
                Status = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        lhp.TrangThaiNhapDiem = true;

        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Data = true,
            Status = true,
            Message = "Bật nhập điểm thành công"
        };
    }

    // Trigger trang thai nhap diem lop hoc phan
    public async Task<ApiResponse<bool>> TriggerTrangThaiNhapDiem(string id)
    {
        // Check Lop Hoc Phan Ton Tai Khong
        var lhp = await _context.LopHocPhans
            .FirstOrDefaultAsync(l => l.IdLopHocPhan == id);
        if (lhp == null)
        {
            return new ApiResponse<bool>
            {
                Data = false,
                Status = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        lhp.TrangThaiNhapDiem = (lhp.TrangThaiNhapDiem == true) ? false : true;

        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Data = true,
            Status = true,
            Message = "Thay đổi trạng thái nhập điểm thành công"
        };
    }

    /**
     * Gán List Sinh Viên Vào Lớp Học Phần
     */
    public async Task<ApiResponse<List<SinhVienDto>>
        > AddListSinhVienLopHocPhan(List<SinhVienDto> listSinhVien, string idLopHocPhan)
    {
        // Check Lop Hoc Phan Ton Tai Khong
        var lhp = await _context.LopHocPhans
            .FirstOrDefaultAsync(l => l.IdLopHocPhan == idLopHocPhan);
        if (lhp == null)
        {
            return new ApiResponse<List<SinhVienDto>>
            {
                Data = null,
                Status = false,
                Message = "Không tìm thấy lớp học phần"
            };
        }

        List<SinhVienDto> listSinhVienError = new List<SinhVienDto>();

        foreach (var sv in listSinhVien)
        {
            // Check Sinh Vien Ton Tai Khong
            var sinhvien = await _context.SinhViens
                .FirstOrDefaultAsync(s => s.IdSinhVien == sv.IdSinhVien);
            if (sinhvien == null)
            {
                sv.TenSinhVien = $"Sinh Viên: {sv.TenSinhVien} lỗi không tìm thấy sinh viên {sv.IdSinhVien}";
                listSinhVienError.Add(sv);
                continue;
            }

            // Check Sinh Vien Da Ton Tai
            var sv_lhp = await _context.SinhVienLopHocPhans
                .FirstOrDefaultAsync(s => s.IdSinhVien == sv.IdSinhVien && s.IdLopHocPhan == idLopHocPhan);
            if (sv_lhp != null)
            {
                sv.TenSinhVien = $"Sinh Viên: {sv.TenSinhVien} lỗi sinh viên đã tồn tại trong lớp học phần";
                listSinhVienError.Add(sv);
                continue;
            }

            // Add Sinh Vien Vao Lop Hoc Phan
            await _context.SinhVienLopHocPhans.AddAsync(new SinhVienLopHocPhan
            {
                IdSinhVien = sv.IdSinhVien,
                IdLopHocPhan = idLopHocPhan
            });

            // Lay Ra So Lan Hoc Cua Sinh Vien Tai Mon Hoc Do
            var soLanHoc = await (
                from sv_lhp in _context.SinhVienLopHocPhans
                where sv_lhp.IdSinhVien == sv.IdSinhVien
                join lhp in _context.LopHocPhans
                    on sv_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
                join mh in _context.MonHocs
                    on lhp.IdMonHoc equals mh.IdMonHoc
                select sv_lhp
            ).CountAsync();

            // Add Diem Sinh Vien
            await _context.Diems.AddAsync(new DiemDto
            {
                IdDiem = Guid.NewGuid().ToString(),
                IdLopHocPhan = idLopHocPhan,
                IdSinhVien = sv.IdSinhVien,
                DiemQuaTrinh = 0,
                DiemKetThuc = 0,
                DiemTongKet = 0,
                LanHoc = soLanHoc + 1,
            });
        }

        if (listSinhVienError.Length > 0)
        {
            return new ApiResponse<List<SinhVienDto>>
            {
                Data = listSinhVienError,
                Status = false,
                Message = "Thêm danh sách sinh viên vào lớp học phần thất bại"
            };
        }

        await _context.SaveChangesAsync();

        return new ApiResponse<List<SinhVienDto>>
        {
            Data = listSinhVien,
            Status = true,
            Message = "Thêm danh sách sinh viên vào lớp học phần thành công"
        };
    }
}
