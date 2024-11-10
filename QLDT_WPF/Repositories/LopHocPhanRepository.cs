using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using Azure.Identity;

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

                TenLopHocPhan = l.TenLopHocPhan,
                TenGiaoVien= gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                ThoiGianBatDau = l.ThoiGianBatDau,
                ThoiGianKetThuc = l.ThoiGianKetThuc,
            }
        ).ToListAsync();

        return new ApiResponse<List<LopHocPhanDto>>{
            Data = lhp,
            Status = true,
            Message = "Lấy dữ liệu thành công"
        };
    }

    /**
     * Lay lop hoc phan by id
     */
    public async Task<ApiResponse<LopHocPhanDto>> GetById(int id)
    {
        // TODO

        return null;
    }

    /**
     * Sua thong tin lop hoc phan
     */
    public async Task<ApiResponse<LopHocPhanDto>> Edit(LopHocPhanDto lopHocPhan)
    {
        //TODO
        return null;
    }

    /**
     * Them lop hoc phan
     */
    public async Task<ApiResponse<LopHocPhanDto>> Add(LopHocPhanDto lopHocPhan)
    {
        // if id lop hoc phan is null -> generate new id
        // TODO
        return null;
    }

    /**
     * Xoa lop hoc phan By Id 
     */
    public async Task<ApiResponse<LopHocPhanDto>> Delete(string id)
    {
        // TODO
        return null;
    }

    /** 
     * Get lop hoc phan cua sinh vien tu id
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>> GetLopHocPhansFromSinhVien(string id)
    {
        // TODO 
        return null;
    }

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