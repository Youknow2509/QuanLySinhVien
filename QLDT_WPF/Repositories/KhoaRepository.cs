using System.Collections.Generic;
using System.Linq;

//
using QLDT_WPF.Data;

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


    /**
     * Lay khoa by id
     */

    /**
     * Lay khoa by id
     */


    /**
     * Sua thong tin khoa
     */


    /**
     * Them khoa
     */


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