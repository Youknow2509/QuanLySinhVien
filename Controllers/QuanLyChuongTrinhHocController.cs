
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
//
using web_qlsv.Models;
using web_qlsv.Data;

namespace web_qlsv.Controllers;

public class QuanLyChuongTrinhHocController : Controller
{
    // Variable
    private readonly ILogger<QuanLyChuongTrinhHocController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLyChuongTrinhHocController(
        ILogger<QuanLyChuongTrinhHocController> logger,
        QuanLySinhVienDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /**
     * GET: /Admin/QuanLyChuongTrinhHoc
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Admin/QuanLyChuongTrinhHoc/Details
    public IActionResult Details(string IdChuongTrinhHoc)
    {
        List<MonHoc> list = (
            from mh in _context.MonHocs
            join cth in _context.ChuongTrinhHocMonHocs
                on mh.IdMonHoc equals cth.IdMonHoc
            where cth.IdChuongTrinhHoc == IdChuongTrinhHoc
            select new MonHoc
            {
                TenMonHoc = mh.TenMonHoc,
                IdMonHoc = mh.IdMonHoc,
                IdKhoa = mh.IdKhoa,
                IdKhoaNavigation = mh.IdKhoaNavigation,
            }
        ).ToList();

        ViewBag.IdChuongTrinhHoc = IdChuongTrinhHoc;
        ViewBag.Title = _context.ChuongTrinhHocs.Find(IdChuongTrinhHoc).TenChuongTrinhHoc;

        return View(list);
    }

    /**
     * GET: /Admin/QuanLyChuongTrinhHoc/DeleteMonFromChuongTrinh
     */
    public IActionResult DeleteMonFromChuongTrinh(string IdMonHoc, string IdChuongTrinhHoc)
    {
        var qr_cch_mh = (
            from cch_mh in _context.ChuongTrinhHocMonHocs
            where cch_mh.IdMonHoc == IdMonHoc && cch_mh.IdChuongTrinhHoc == IdChuongTrinhHoc
            select cch_mh
        ).FirstOrDefault();

        if (qr_cch_mh == null || qr_cch_mh.IdMonHoc == null || qr_cch_mh.IdChuongTrinhHoc == null)
        {
            return NotFound();
        }

        _context.ChuongTrinhHocMonHocs.Remove(qr_cch_mh);

        return RedirectToAction("Details", new { IdChuongTrinhHoc });
    }

    // GET: /Admin/QuanLyChuongTrinhHoc/AddMonHocToChuongTrinh
    public IActionResult AddMonHocToChuongTrinh(string IdChuongTrinhHoc)
    {
        ViewBag.IdChuongTrinhHoc = IdChuongTrinhHoc;
        return View();
    }

    // POST: /Admin/QuanLyChuongTrinhHoc/AddMonHocToChuongTrinh
    [HttpPost]
    public IActionResult AddMonHocToChuongTrinh(string IdChuongTrinhHoc, List<string> IdMonHoc)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
