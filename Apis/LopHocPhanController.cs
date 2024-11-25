using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

//
using web_qlsv.Data;
using web_qlsv.Models;
using web_qlsv.Dto;

namespace web_qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LopHocPhanController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    // Constructor
    public LopHocPhanController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /** 
     * GET: api/lophocphan/
     * Get tat ca lop hoc phan
     */
    [HttpGet]
    public async Task<IActionResult> GetLopHocPhans()
    {
        // Query
        var query = await (
            from lhp in _context.LopHocPhans
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mh in _context.MonHocs on lhp.IdMonHoc equals mh.IdMonHoc
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mh.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mh.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/lophocphan/sinhvien/{id}
     * Get lop hoc phan cua sinh vien tu id
     */
    [HttpGet("sinhvien/{id}")]
    public async Task<IActionResult> GetLopHocPhanBySinhVien(string id)
    {
        // Query
        var query = await (
            from svlhp in _context.SinhVienLopHocPhans
            join lhp in _context.LopHocPhans on svlhp.IdLopHocPhan equals lhp.IdLopHocPhan
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mon in _context.MonHocs on lhp.IdMonHoc equals mon.IdMonHoc
            where svlhp.IdSinhVien == id
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mon.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mon.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/lophocphan/{id}
     * Get lop hoc phan cua giao vien tu id
     */
    [HttpGet("giaovien/{id}")]
    public async Task<IActionResult> GetLopHocPhanByGiaoVien(string id)
    {
        // Query
        var query = await (
            from lhp in _context.LopHocPhans
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mon in _context.MonHocs on lhp.IdMonHoc equals mon.IdMonHoc
            where gv.IdGiaoVien == id
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mon.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mon.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * GET: api/lophocphan/{IdLopHocPhan}
     * Get lop hoc phan by id
     */
    [HttpGet("{IdLopHocPhan}")]
    public async Task<IActionResult> GetLopHocPhan(string IdLopHocPhan)
    {
        // Query
        var query = await (
            from lhp in _context.LopHocPhans
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            join mon in _context.MonHocs on lhp.IdMonHoc equals mon.IdMonHoc
            where lhp.IdLopHocPhan == IdLopHocPhan
            select new
            {
                TenLopHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                TenMonHoc = mon.TenMonHoc,
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = mon.IdMonHoc,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).FirstOrDefaultAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * DELETE: api/lophocphan/{IdLopHocPhan}
     * Delete lop hoc phan by IdLopHocPhan
     */
    [HttpDelete("{IdLopHocPhan}")]
    public async Task<IActionResult> DeleteLopHocPhan(string IdLopHocPhan)
    {

        // find lhp
        var qr = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == IdLopHocPhan
            select lhp
        ).FirstOrDefaultAsync();

        if (qr == null)
        {
            return NotFound("Không tìm thấy lớp học phần");
        }

        _context.LopHocPhans.Remove(qr);
        _context.SaveChanges();

        return Ok();
    }


    /**
     * POST: api/lophocphan/
     * Create lop hoc phan
     */
    [HttpPost]
    public async Task<IActionResult> CreateLopHocPhan([FromBody] LopHocPhanDto lopHocPhan)
    {
        // if id lop hoc phan is null -> generate new id
        if (lopHocPhan.IdLopHocPhan == null)
        {
            lopHocPhan.IdLopHocPhan = Guid.NewGuid().ToString();
        }

        // Check thoi gian
        if (lopHocPhan.ThoiGianBatDau >= lopHocPhan.ThoiGianKetThuc)
        {
            return BadRequest("Thời gian không hợp lệ");
        }

        // Xử lí lớp học phần tạo chỉ được tạo ở tương lai
        if (lopHocPhan.ThoiGianBatDau < DateTime.Now
            || lopHocPhan.ThoiGianKetThuc < DateTime.Now)
        {
            return BadRequest("Thời gian truyền vào ở quá khứ");
        }

        // Create new lop hoc phan
        var newLopHocPhan = new LopHocPhan
        {
            IdLopHocPhan = lopHocPhan.IdLopHocPhan,
            TenHocPhan = lopHocPhan.TenLopHocPhan,
            IdGiaoVien = lopHocPhan.IdGiaoVien,
            IdMonHoc = lopHocPhan.IdMonHoc,
            ThoiGianBatDau = lopHocPhan.ThoiGianBatDau ?? DateTime.MinValue,
            ThoiGianKetThuc = lopHocPhan.ThoiGianKetThuc ?? DateTime.MinValue,
        };

        // Check giao vien, mon hoc
        var mon = await _context.MonHocs.FindAsync(lopHocPhan.IdMonHoc);
        if (mon == null)
        {
            return BadRequest("Không tìm thấy môn học");
        }
        var gv = await _context.GiaoViens.FindAsync(lopHocPhan.IdGiaoVien);
        if (gv == null)
        {
            return BadRequest("Không tìm thấy giáo viên");
        }

        // Add new lop hoc phan
        _context.LopHocPhans.Add(newLopHocPhan);
        _context.SaveChanges();

        return Ok(new
        {
            statusCode = 200,
            messages = "Tạo lớp học phần thành công",
            data = new
            {
                IdLopHocPhan = newLopHocPhan.IdLopHocPhan,
                TenLopHocPhan = newLopHocPhan.TenHocPhan,
                IdGiaoVien = newLopHocPhan.IdGiaoVien,
                IdMonHoc = newLopHocPhan.IdMonHoc,
                ThoiGianBatDau = newLopHocPhan.ThoiGianBatDau,
                ThoiGianKetThuc = newLopHocPhan.ThoiGianKetThuc,
            }
        });
    }

    /**
     * PUT: api/lophocphan/{IdLopHocPhan}
     * Update lop hoc phan by IdLopHocPhan
     */
    [HttpPut("{IdLopHocPhan}")]
    public async Task<IActionResult> UpdateLopHocPhan(
        string IdLopHocPhan,
        [FromBody] LopHocPhanDto lopHocPhan)
    {
        // find lhp
        var qr = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == IdLopHocPhan
            select lhp
        ).FirstOrDefaultAsync();

        if (qr == null)
        {
            return NotFound("Không tìm thấy lớp học phần");
        }

        // Update lop hoc phan
        qr.TenHocPhan = lopHocPhan.TenLopHocPhan;
        qr.IdGiaoVien = lopHocPhan.IdGiaoVien;
        qr.IdMonHoc = lopHocPhan.IdMonHoc;
        qr.ThoiGianBatDau = lopHocPhan.ThoiGianBatDau ?? DateTime.MinValue;
        qr.ThoiGianKetThuc = lopHocPhan.ThoiGianKetThuc ?? DateTime.MinValue;

        // Check giao vien, mon hoc
        var mon = await _context.MonHocs.FindAsync(lopHocPhan.IdMonHoc);
        if (mon == null)
        {
            return BadRequest("Không tìm thấy môn học");
        }
        var gv = await _context.GiaoViens.FindAsync(lopHocPhan.IdGiaoVien);
        if (gv == null)
        {
            return BadRequest("Không tìm thấy giáo viên");
        }

        // Update
        _context.LopHocPhans.Update(qr);
        _context.SaveChanges();

        return Ok(new
        {
            statusCode = 200,
            messages = "Cạp nhật thành công",
            data = new
            {
                IdLopHocPhan = qr.IdLopHocPhan,
                TenLopHocPhan = qr.TenHocPhan,
                IdGiaoVien = qr.IdGiaoVien,
                IdMonHoc = qr.IdMonHoc,
                ThoiGianBatDau = qr.ThoiGianBatDau,
                ThoiGianKetThuc = qr.ThoiGianKetThuc
            }
        });
    }

    /**
     * GET: api/monhoc/{idLopHocPhan}/lophocphan/
     * Get lop hoc phan from id mon hoc
     */
    [HttpGet("{idLopHocPhan}/lophocphan")]
    public async Task<IActionResult> GetLopHocPhanFromMonHoc(string idLopHocPhan)
    {
        // query
        var qr = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == idLopHocPhan
            join gv in _context.GiaoViens on lhp.IdGiaoVien equals gv.IdGiaoVien
            select new
            {
                IdLopHocPhan = lhp.IdLopHocPhan,
                IdGiaoVien = gv.IdGiaoVien,
                IdMonHoc = lhp.IdMonHoc,
                TenHocPhan = lhp.TenHocPhan,
                TenGiaoVien = gv.TenGiaoVien,
                ThoiGianBatDau = lhp.ThoiGianBatDau,
                ThoiGianKetThuc = lhp.ThoiGianKetThuc
            }
        ).ToListAsync();

        return Ok(qr);
    }

    /**
     * PUT: api/lophocphan/thaydoithoigian
     * Thay doi thoi gian lop hoc phan 
     */
    [HttpPut("thaydoithoigian")]
    public async Task<IActionResult> UpdateThoiGianLopHocPhan(
        [FromBody] ThayDoiThoiGianLopHocPhanDto thayDoiThoiGian)
    {
        // find lhp
        ThoiGian? tg = await _context.ThoiGians.FirstOrDefaultAsync(x =>
            x.IdThoiGian == thayDoiThoiGian.IdThoiGian);
        // find lhp
        LopHocPhan? lhp = await _context.LopHocPhans.FirstOrDefaultAsync(x =>
            x.IdLopHocPhan == thayDoiThoiGian.IdLopHocPhan);

        if (tg == null)
        {
            return NotFound("Không tìm thấy thời gian");
        }
        if (lhp == null)
        {
            return NotFound("Không tìm thấy lớp học phần");
        }

        // Check thoi gian co thoa man khong
        if (thayDoiThoiGian.ThoiGianBatDau >= thayDoiThoiGian.ThoiGianKetThuc)
        {
            return BadRequest("Thời gian không hợp lệ");
        }

        // Xử Lí Thời Gian Truyền Vào Ở Quá Khứ
        if (thayDoiThoiGian.ThoiGianBatDau < DateTime.Now || thayDoiThoiGian.ThoiGianKetThuc < DateTime.Now)
        {
            return BadRequest("Thời gian truyền vào ở quá khứ");
        }

        // check thoi gian lophoc phan co bi trung khong 
        var th_lhp_check = (from tl in _context.ThoiGianLopHocPhans
                            join tgCheck in _context.ThoiGians
                                on tl.IdThoiGian equals tgCheck.IdThoiGian
                            where tl.IdLopHocPhan == thayDoiThoiGian.IdLopHocPhan &&
                                (
                                    (thayDoiThoiGian.ThoiGianBatDau < tgCheck.NgayKetThuc && thayDoiThoiGian.ThoiGianBatDau > tgCheck.NgayBatDau)
                                    ||
                                    (thayDoiThoiGian.ThoiGianKetThuc < tgCheck.NgayKetThuc && thayDoiThoiGian.ThoiGianKetThuc > tgCheck.NgayBatDau)
                                )
                            select tgCheck).Any();
        if (th_lhp_check)
        {
            return BadRequest("Thời gian bị trùng lịch hiện tại");
        }
        // Check thoi trong khoang cho phep
        if (lhp.ThoiGianBatDau > thayDoiThoiGian.ThoiGianBatDau ||
            lhp.ThoiGianKetThuc < thayDoiThoiGian.ThoiGianKetThuc)
        {
            return BadRequest("Thời gian không nằm trong khoảng cho phép");
        }

        // Update thoi gian
        tg.NgayBatDau = thayDoiThoiGian.ThoiGianBatDau;
        tg.NgayKetThuc = thayDoiThoiGian.ThoiGianKetThuc;
        // tg.DiaDiem = thayDoiThoiGian.DiaDiem;
        // TODO

        // Update
        _context.ThoiGians.Update(tg);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            statusCode = 200,
            messages = "Cập nhật thành công",
            data = new
            {
                IdThoiGian = tg.IdThoiGian,
                NgayBatDau = tg.NgayBatDau,
                NgayKetThuc = tg.NgayKetThuc,
                // DiaDiem = tg.DiaDiem, 
                // TODO
                IdLopHocPhan = lhp.IdLopHocPhan
            }
        });
    }

    /**
     * POST: /api/lophocphan/themthoigian
     * Thêm thời gian cho lớp học phần
     */
    [HttpPost("themthoigian")]
    public async Task<IActionResult> CreateThoiGianLopHocPhan(TaoThoiGianLopHocPhanDto thoigian)
    {
        // if id thoigian is null
        if (thoigian.IdThoiGian == null)
        {
            thoigian.IdThoiGian = Guid.NewGuid().ToString();
        }
        // find lhp
        LopHocPhan? lhp = await _context.LopHocPhans.FirstOrDefaultAsync(x =>
            x.IdLopHocPhan == thoigian.IdLopHocPhan);
        if (lhp == null)
        {
            return NotFound("Không tìm thấy lớp học phần");
        }
        // find id thoi gian 
        ThoiGian? tg = await _context.ThoiGians.FirstOrDefaultAsync(x =>
            x.IdThoiGian == thoigian.IdThoiGian);
        if (tg != null)
        {
            thoigian.IdThoiGian = Guid.NewGuid().ToString();
        }

        // Check thoi gian co thoa man khong
        if (thoigian.ThoiGianBatDau >= thoigian.ThoiGianKetThuc)
        {
            return BadRequest("Thời gian không hợp lệ");
        }

        // Xử Lí Thời Gian Truyền Vào Ở Quá Khứ
        if (thoigian.ThoiGianBatDau < DateTime.Now || thoigian.ThoiGianKetThuc < DateTime.Now)
        {
            return BadRequest("Thời gian truyền vào ở quá khứ");
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
                            select tgCheck).Any();
        // Check thoi trong khoang cho phep
        if (lhp.ThoiGianBatDau > thoigian.ThoiGianBatDau ||
            lhp.ThoiGianKetThuc < thoigian.ThoiGianKetThuc)
        {
            return BadRequest("Thời gian không nằm trong khoảng cho phép");
        }

        // Create new thoi gian
        ThoiGian newThoiGian = new ThoiGian
        {
            IdThoiGian = thoigian.IdThoiGian,
            NgayBatDau = thoigian.ThoiGianBatDau,
            NgayKetThuc = thoigian.ThoiGianKetThuc,
            // DiaDiem = thoigian.DiaDiem
            // TODO
        };

        // Add new thoi gian
        _context.ThoiGians.Add(newThoiGian);
        // Add new thoi gian lop hoc phan
        _context.ThoiGianLopHocPhans.Add(new ThoiGianLopHocPhan
        {
            IdThoiGian = newThoiGian.IdThoiGian,
            IdLopHocPhan = lhp.IdLopHocPhan
        });

        await _context.SaveChangesAsync();

        return Ok("Thời gian lớp học phần đã được thêm");
    }
}