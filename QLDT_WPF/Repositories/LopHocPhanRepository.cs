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
        // todo 
        return null;
    }

    /**
     * Lay lop hoc phan by id
     */
    public async Task<ApiResponse<LopHocPhanDto>> GetById(int id)
    {
        // todo

        return null;
    }

    /**
     * Sua thong tin lop hoc phan
     */
    public async Task<ApiResponse<LopHocPhanDto>> Edit(LopHocPhanDto lopHocPhan)
    {
        //todo
        return null;
    }

    /**
     * Them lop hoc phan
     */
    public async Task<ApiResponse<LopHocPhanDto>> Add(LopHocPhanDto lopHocPhan)
    {
        // if id lop hoc phan is null -> generate new id
        // todo
        return null;
    }

    /**
     * Xoa lop hoc phan By Id 
     */
    public async Task<ApiResponse<LopHocPhanDto>> Delete(string id)
    {
        // todo
        return null;
    }

    /** 
     * Get lop hoc phan cua sinh vien tu id
     */
    public async Task<ApiResponse<List<LopHocPhanDto>>> GetLopHocPhansFromSinhVien(string id)
    {
        // todo 
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