using System.Collections.Generic;
using System.Linq;

//
using QLDT_WPF.Data;

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