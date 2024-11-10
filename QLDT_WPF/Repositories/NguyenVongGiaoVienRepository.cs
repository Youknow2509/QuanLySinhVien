using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

//
using QLDT_WPF.Data;
using QLDT_WPF.Dto;
using QLDT_WPF.Models;

namespace QLDT_WPF.Repositories;

public class NguyenVongGiaoVienRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public NguyenVongGiaoVienRepository()
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
     * Lay tat ca nguyen vong cua giao vien
     */


    /**
     * Lay nguyen vong cua giao vien by id
     */

    /**
     * Lay nguyen vong cua giao vien by id
     */


    /**
     * Sua thong tin nguyen vong cua giao vien
     */


    /**
     * Them nguyen vong cua giao vien
     */


    /**
     * Xoa nguyen vong cua giao vien By Id 
     */

    /**
     * Lay nguyen vong by id giao vien
     */

    /**
     * Chap nhan nguyen vong by id
     */

    /**
     * Tu choi nguyen vong by id
     */
    
}