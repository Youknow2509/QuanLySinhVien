
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using web_qlsv.Models;
using web_qlsv.Data;

namespace web_qlsv.Controllers;

public class QuanLyMonHocController : Controller
{
    // Variable
    private readonly ILogger<QuanLyMonHocController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLyMonHocController(
        ILogger<QuanLyMonHocController> logger,
        QuanLySinhVienDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    /**
     * GET: /Admin/QuanLyMonHoc
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    /**
     * GET: /Admin/QuanLyMonHoc/Details
     * Details Page
     */
    public IActionResult Details(string IdMonHoc)
    {
        var monHoc = _context.MonHocs.FirstOrDefault(x => x.IdMonHoc == IdMonHoc);;
        if (monHoc == null)
        {
            return NotFound();
        }
        return View(monHoc);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
