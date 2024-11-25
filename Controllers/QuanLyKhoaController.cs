
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using web_qlsv.Models;
using web_qlsv.Data;

namespace web_qlsv.Controllers;

[Area("Admin")]
public class QuanLyKhoaController : Controller
{
    // Variable
    private readonly ILogger<QuanLyKhoaController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLyKhoaController(
        ILogger<QuanLyKhoaController> logger,
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _logger = logger;
        _context = quanLySinhVienDbContext;
    }

    /**
     * GET: /Admin/QuanLyKhoa
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    /**
     * GET: /Admin/QuanLyKhoa/Deatail
     * Xem thong tin chi tiet trong khoa
     */
    public IActionResult Details(string idKhoa)
    {

        // variables
        Khoa? qr = _context.Khoas.Where(k => k.IdKhoa == idKhoa).FirstOrDefault();
        if (qr == null)
        {
            return NotFound();
        }
        return View(qr);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
