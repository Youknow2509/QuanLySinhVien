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

    /**
     * Lay lop hoc phan by id
     */


    /**
     * Sua thong tin lop hoc phan
     */


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