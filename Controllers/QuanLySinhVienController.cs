
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


using web_qlsv.Dto;
using web_qlsv.Models;
using web_qlsv.Data;

namespace web_qlsv.Controllers;

public class QuanLySinhVienController : Controller
{
    // Variable
    private readonly ILogger<QuanLySinhVienController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLySinhVienController(
        ILogger<QuanLySinhVienController> logger,
        QuanLySinhVienDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /**
     * GET: /Admin/QuanLySinhVien
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    /**
     * GET: /Admin/QuanLySinhVien/Details?IdSinhVien={IdSinhVien}
     * get details sinh vien
     */
    public IActionResult Details(string IdSinhVien)
    {
        SinhVien? sv = _context.SinhViens
            .Include(x => x.IdChuongTrinhHocNavigation)
            .Include(x => x.IdKhoaNavigation)
            .FirstOrDefault(x => x.IdSinhVien == IdSinhVien);
        if (sv == null)
        {
            return NotFound();
        }

        return View(sv);
    }

    /**
     * GET: /Admin/QuanLySinhVie/Edit?IdSinhVien={IdSinhVien}
     * Edit sinh vien information
     */
    public IActionResult Edit(string IdSinhVien)
    {
        SinhVien? sv = _context.SinhViens
            .Include(x => x.IdChuongTrinhHocNavigation)
            .Include(x => x.IdKhoaNavigation)
            .FirstOrDefault(x => x.IdSinhVien == IdSinhVien);
        if (sv == null)
        {
            return NotFound();
        }

        return View(sv);
    }

    /**
     * POST: /Admin/QuanLySinhVien/Edit
     * POST Edit sinh vien information
     */
    [HttpPost]
    public async Task<IActionResult> Edit(SinhVien sv)
    {
        var qr = _context.SinhViens
            .Include(x => x.IdChuongTrinhHocNavigation)
            .Include(x => x.IdKhoaNavigation)
            .FirstOrDefault(x => x.IdSinhVien == sv.IdSinhVien);
        if (qr == null)
        {
            return NotFound();
        }

        qr.HoTen = sv.HoTen;
        qr.Lop = sv.Lop;
        qr.DiaChi = sv.DiaChi;
        qr.NgaySinh = sv.NgaySinh;

        _context.SaveChanges();
        return RedirectToAction("Details", new { IdSinhVien = sv.IdSinhVien });
    }

    /**
     * POST: /Admin/QuanLyGiaoVien/UploadCSV
     * Upload CSV file create list giao vien
     */
    [HttpPost]
    public async Task<IActionResult> UploadCSV(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File not found");
        }

        var sinhviens = new List<SinhVien>();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(",");
                var idCTH = _context.ChuongTrinhHocs.FirstOrDefault(x => x.TenChuongTrinhHoc == values[6].Trim());
                if (idCTH == null)
                {
                    return BadRequest("Chuong trinh hoc not found");
                }
                var sv = new SinhVienDto
                {
                    IdSinhVien = values[0].Trim(),
                    HoTen = values[1].Trim(),
                    Lop = values[2].Trim(),
                    NgaySinh = DateTime.Parse(values[3].Trim()),
                    DiaChi = values[4].Trim(),
                    IdKhoa = values[5].Trim(),
                    IdChuongTrinhHoc = idCTH.IdChuongTrinhHoc,
                    Email = values[7].Trim(),
                    SoDienThoai = values[8].Trim()
                };
                if (SinhVienExists(sv).Status)
                {
                    sinhviens.Add(new SinhVien
                    {
                        IdSinhVien = sv.IdSinhVien,
                        HoTen = sv.HoTen,
                        Lop = sv.Lop,
                        NgaySinh = sv.NgaySinh ?? DateTime.MinValue,
                        DiaChi = sv.DiaChi,
                        IdKhoa = sv.IdKhoa,
                        IdChuongTrinhHoc = sv.IdChuongTrinhHoc
                    });
                }
                else
                {
                    return BadRequest(SinhVienExists(sv).Message);
                }
            }
        }

        _context.SinhViens.AddRange(sinhviens);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // Helper check information of user
    private StatusUploadFileDto SinhVienExists(SinhVienDto _sinhVien)
    {
        // Check id
        var id = _sinhVien.IdSinhVien;
        // Check email
        string? email = _sinhVien.Email;
        // Check null email in identity context
        // Check so dien thoai
        string? soDienThoai = _sinhVien.SoDienThoai;
        return new StatusUploadFileDto
        {
            Status = true,
            Message = "Success"
        };
    }

}
