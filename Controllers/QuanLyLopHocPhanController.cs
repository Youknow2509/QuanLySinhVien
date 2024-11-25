
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//
using web_qlsv.Models;
using web_qlsv.Data;
using web_qlsv.Dto;

namespace web_qlsv.Controllers;

public class QuanLyLopHocPhanController : Controller
{
    // Variable
    private readonly ILogger<QuanLyLopHocPhanController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLyLopHocPhanController(
        ILogger<QuanLyLopHocPhanController> logger,
        QuanLySinhVienDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /**
     * GET: /Admin/QuanLyLopHocPhan
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Admin/QuanLyLopHocPhan/Details?IdLopHocPhan={id}
    public IActionResult Details(string IdLopHocPhan)
    {

        var lhp = _context.LopHocPhans
            .Include(x => x.IdMonHocNavigation)
            .Include(x => x.IdGiaoVienNavigation)
            .FirstOrDefault(x => x.IdLopHocPhan == IdLopHocPhan);

        return View(lhp);
    }

    /**
     * POST: QuanLyLopHocPhan/UploadThoiGianCSV
     * UploadThoiGianCSV: Xử lý upload file csv thời gian học vào lớp học phần
     */
    [HttpPost]
    public async Task<IActionResult> UploadThoiGianCSV(IFormFile file, string IdLopHocPhan)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File not found");
        }
        var listThoiGian = new List<TaoThoiGianLopHocPhanDto>();
        var listThoiGian_LopHocPhan = new List<ThoiGianLopHocPhan>();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(",");

                var thoiGian = new TaoThoiGianLopHocPhanDto
                {
                    IdThoiGian = Guid.NewGuid().ToString(),
                    IdLopHocPhan = IdLopHocPhan,
                    ThoiGianBatDau = DateTime.Parse(values[0]),
                    ThoiGianKetThuc = DateTime.Parse(values[1]),
                    IdPhongHoc = values[2],
                };
                if (ThoiGianLopHocPhanExists(thoiGian).Status)
                {
                    listThoiGian.Add(thoiGian);
                }
                else
                {
                    return BadRequest(ThoiGianLopHocPhanExists(thoiGian).Message);
                }
            }
        }

        // add thoi gian
        _context.ThoiGians.AddRange(listThoiGian.Select(x => new ThoiGian
        {
            IdThoiGian = x.IdThoiGian,
            NgayBatDau = x.ThoiGianBatDau,
            NgayKetThuc = x.ThoiGianKetThuc,
            IdPhongHoc = x.IdPhongHoc,
        }));
        // gan thoi gian vao lop hoc phan
        _context.ThoiGianLopHocPhans.AddRange(listThoiGian.Select(x => new ThoiGianLopHocPhan
        {
            IdThoiGianLopHocPhan = Guid.NewGuid().ToString(),
            IdThoiGian = x.IdThoiGian,
            IdLopHocPhan = x.IdLopHocPhan
        }));

        _context.SaveChanges();

        return RedirectToAction("Details", new { IdLopHocPhan = listThoiGian.FirstOrDefault().IdLopHocPhan });
    }

    private StatusUploadFileDto ThoiGianLopHocPhanExists(TaoThoiGianLopHocPhanDto thoigian)
    {
        // Check id
        var id = thoigian.IdLopHocPhan;
        var lopHp = _context.LopHocPhans.FirstOrDefault(x => x.IdLopHocPhan == id);
        if (lopHp == null)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Không tồn tại lớp học phần"
            };
        }
        // Check thoi gian co thoa man khong
        if (thoigian.ThoiGianBatDau >= thoigian.ThoiGianKetThuc)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc"
            };
        }

        // Xử Lí Thời Gian Truyền Vào Ở Quá Khứ
        if (thoigian.ThoiGianBatDau < DateTime.Now || thoigian.ThoiGianKetThuc < DateTime.Now)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Thời gian không thể ở quá khứ"
            };
        }

        // check thoi gian lophoc phan co bi trung khong 
        var th_lhp_check = (from tl in _context.ThoiGianLopHocPhans
                            join tgCheck in _context.ThoiGians
                                on tl.IdThoiGian equals tgCheck.IdThoiGian
                            where tl.IdLopHocPhan == thoigian.IdLopHocPhan &&
                                (
                                    (thoigian.ThoiGianBatDau < tgCheck.NgayKetThuc && thoigian.ThoiGianBatDau > tgCheck.NgayBatDau)
                                    ||
                                    (thoigian.ThoiGianKetThuc < tgCheck.NgayKetThuc && thoigian.ThoiGianKetThuc > tgCheck.NgayBatDau)
                                )
                            select tgCheck).ToList();
        if (th_lhp_check.Count > 0)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Thời gian không được trùng với thời gian khác"
            };
        }
        // Check thoi trong khoang cho phep
        if (lopHp.ThoiGianBatDau > thoigian.ThoiGianBatDau ||
            lopHp.ThoiGianKetThuc < thoigian.ThoiGianKetThuc)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Thời gian không nằm trong khoảng thời gian của lớp học phần"
            };
        }

        return new StatusUploadFileDto
        {
            Status = true,
            Message = "Success"
        };
    }

    /**
     * POST: /Admin/QuanLyLopHocPhan/UploadSinhVienCSV
     * UploadSinhVienCSV: Xử lý upload file csv lớp học phần
     */
    [HttpPost]
    public async Task<IActionResult> UploadSinhVienCSV(IFormFile file, string IdLopHocPhan)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File not found");
        }
        var listSinhVien = new List<SinhVienLopHocPhan>();
        var listDiem = new List<Diem>();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(",");
                var sinhVien = new SinhVienLopHocPhan
                {
                    IdSinhVienLopHocPhan = Guid.NewGuid().ToString(),
                    IdSinhVien = values[0],
                    IdLopHocPhan = IdLopHocPhan
                };
                if (SinhVienExists(sinhVien).Status)
                {
                    listSinhVien.Add(sinhVien);
                    listDiem.Add(new Diem
                    {
                        IdDiem = Guid.NewGuid().ToString(),
                        IdLopHocPhan = IdLopHocPhan,
                        IdSinhVien = values[0],
                        DiemQuaTrinh = 0,
                        DiemKetThuc = 0,
                        DiemTongKet = 0,
                        LanHoc = 1
                    });
                }
                else
                {
                    return BadRequest(SinhVienExists(sinhVien).Message);
                }
            }
        }

        // add sinh vien
        _context.SinhVienLopHocPhans.AddRange(listSinhVien);

        // add diem
        _context.Diems.AddRange(listDiem);

        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { IdLopHocPhan = listSinhVien.FirstOrDefault().IdLopHocPhan });
    }

    private StatusUploadFileDto SinhVienExists(SinhVienLopHocPhan svlhp)
    {
        // check sinh vien exists
        var sv = _context.SinhViens.FirstOrDefault(x => x.IdSinhVien == svlhp.IdSinhVien);
        if (sv == null)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Không tồn tại sinh viên"
            };
        }

        return new StatusUploadFileDto
        {
            Status = true,
            Message = "Success"
        };
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
