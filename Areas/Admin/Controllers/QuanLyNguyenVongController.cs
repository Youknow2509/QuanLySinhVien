
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyNguyenVongController : Controller
{
    // Variable
    private readonly ILogger<QuanLyNguyenVongController> _logger;
    

    // Constructor
    public QuanLyNguyenVongController(
        ILogger<QuanLyNguyenVongController> logger
    ) {
        _logger = logger;
    }

    /**
     * GET: /Admin/QuanLyNguyenVong
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
