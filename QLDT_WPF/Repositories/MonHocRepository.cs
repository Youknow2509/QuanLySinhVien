using System.Collections.Generic;
using System.Linq;

//
using QLDT_WPF.Data;

namespace QLDT_WPF.Repositories;

public class MonHocRepository
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public MonHocRepository()
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
     * Lay tat ca mon hoc
     */


    /**
     * Lay mon hoc by id
     */

    /**
     * Lay mon hoc by id
     */


    /**
     * Sua thong tin mon hoc
     */


    /**
     * Them mon hoc
     */


    /**
     * Xoa mon hoc By Id 
     */

    /**
     * Get data mon hoc for giao vien with id giao vien
     */

}