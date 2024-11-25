
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//
using web_qlsv.Data;
using web_qlsv.Models;
using web_qlsv.Dto;

namespace web_qlsv.Controllers;

public class QuanLyGiaoVienController : Controller
{
    // Variable
    private readonly ILogger<QuanLyGiaoVienController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLyGiaoVienController(
        ILogger<QuanLyGiaoVienController> logger,
        QuanLySinhVienDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /**
     * GET: /QuanLyGiaoVien
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    // GET: /QuanLyGiaoVien/{id}
    public IActionResult Details(string IdGiaoVien)
    {
        var gv = _context.GiaoViens
                        .Include(x => x.IdKhoaNavigation)
                        .FirstOrDefault(g => g.IdGiaoVien == IdGiaoVien);
        return View(gv);
    }

    // GET: /QuanLyGiaoVien/Edit/{id}
    public IActionResult Edit(string IdGiaoVien)
    {
        var giaovien = (
            from gv in _context.GiaoViens
            where gv.IdGiaoVien == IdGiaoVien
            join khoa in _context.Khoas on gv.IdKhoa equals khoa.IdKhoa
            select new GiaoVien
            {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                SoDienThoai = gv.SoDienThoai,
                Email = gv.Email,
                IdKhoa = gv.IdKhoa,
                IdKhoaNavigation = khoa
            }
        ).FirstOrDefault();

        // Pass TempData values to ViewBag for display
        ViewBag.MessageUpLoadAvatar = TempData["MessageUpLoadAvatar"];
        ViewBag.StatusUpdateAvatar = TempData["StatusUpdateAvatar"];

        return View(giaovien);
    }

    // POST: /QuanLyGiaoVien/Edit/{id}
    [HttpPost]
    public IActionResult Edit(GiaoVien gv)
    {
        if (ModelState.IsValid)
        {
            _context.Update(gv);
            _context.SaveChanges();
            return RedirectToAction("Details", new { IdGiaoVien = gv.IdGiaoVien });
        }
        return View(gv);
    }


    /**
     * POST: /QuanLyGiaoVien/UploadCSV
     * Upload CSV file create list giao vien
     */
    [HttpPost]
    public async Task<IActionResult> UploadCSV(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File not found");
        }

        var giaoviens = new List<GiaoVien>();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(",");
                var gv = new GiaoVienDto
                {
                    IdGiaoVien = values[0].Trim(),
                    TenGiaoVien = values[1].Trim(),
                    SoDienThoai = values[2].Trim(),
                    Email = values[3].Trim(),
                    IdKhoa = values[4].Trim()
                };
                // if (GiaoVienExists(gv).Status)
                // {
                //     giaoviens.Add(new GiaoVien
                //     {
                //         IdGiaoVien = gv.IdGiaoVien,
                //         TenGiaoVien = gv.TenGiaoVien,
                //         SoDienThoai = gv.SoDienThoai,
                //         Email = gv.Email,
                //         IdKhoa = gv.IdKhoa
                //     });
                // }
                // else
                // {
                //     return BadRequest(GiaoVienExists(gv).Message);
                // }
            }
        }

        _context.GiaoViens.AddRange(giaoviens);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
