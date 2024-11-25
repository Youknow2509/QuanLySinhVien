using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

//
using web_qlsv.Data;
using web_qlsv.Models;
using Microsoft.EntityFrameworkCore;
using web_qlsv.Dto;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MonHocController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _quanLySinhVienDbContext;

    // Constructor
    public MonHocController(
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _quanLySinhVienDbContext = quanLySinhVienDbContext;
    }

    /**
     * GET: api/monhoc
     * Get all mon hoc
     */
    [HttpGet]
    public async Task<IActionResult> GetMonHocs()
    {
        var monhocs = await (
            from mh in _quanLySinhVienDbContext.MonHocs
            join khoa in _quanLySinhVienDbContext.Khoas on mh.IdKhoa equals khoa.IdKhoa
            select new
            {
                IdMonHoc = mh.IdMonHoc,
                IdKhoa = khoa.IdKhoa,

                TenMonHoc = mh.TenMonHoc.ToString(),
                TenKhoa = khoa.TenKhoa,
            }
        ).ToListAsync();

        return Ok(monhocs);
    }

    /**
     * GET: api/monhoc/{id}
     * GET mon hoc by ID
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMonHoc(string id)
    {
        var monhoc = await (
            from mh in _quanLySinhVienDbContext.MonHocs
            join khoa in _quanLySinhVienDbContext.Khoas on mh.IdKhoa equals khoa.IdKhoa
            where mh.IdMonHoc == id
            select new
            {
                IdMonHoc = mh.IdMonHoc,
                IdKhoa = khoa.IdKhoa,

                TenMonHoc = mh.TenMonHoc.ToString(),
                TenKhoa = khoa.TenKhoa,
            }
        ).FirstOrDefaultAsync();

        if (monhoc == null)
        {
            return NotFound();
        }

        return Ok(monhoc);
    }

    /**
     * POST: api/monhoc
     * Create new mon hoc
     */
    [HttpPost]
    public async Task<IActionResult> CreateMonHoc(MonHocDto monhoc)
    {
        if (monhoc.IdMonHoc == null)
        {
            monhoc.IdMonHoc = Guid.NewGuid().ToString();
        }

        MonHoc mh = new MonHoc
        {
            IdMonHoc = monhoc.IdMonHoc,
            TenMonHoc = monhoc.TenMonHoc,
            IdKhoa = monhoc.IdKhoa
        };

        _quanLySinhVienDbContext.MonHocs.Add(mh);
        await _quanLySinhVienDbContext.SaveChangesAsync();

        return Ok(new
        {
            StatusCode = 200,
            Message = "Create mon hoc successfully!",
            Data = new
            {
                IdMonHoc = mh.IdMonHoc,
                TenMonHoc = mh.TenMonHoc,
                IdKhoa = mh.IdKhoa
            }
        });
    }

    /**
     * PUT: api/monhoc/{id}
     * Update mon hoc by ID
     */
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMonHoc(string id, MonHocDto monhoc)
    {
        if (id != monhoc.IdMonHoc)
        {
            return BadRequest();
        }

        MonHoc mh = new MonHoc
        {
            IdMonHoc = monhoc.IdMonHoc,
            TenMonHoc = monhoc.TenMonHoc,
            IdKhoa = monhoc.IdKhoa
        };
        // Check id khoa
        var khoa = await _quanLySinhVienDbContext.Khoas.FindAsync(monhoc.IdKhoa);
        if (khoa == null)
        {
            return NotFound(new
            {
                StatusCode = 404,
                Message = "Id khoa not found!"
            });
        }

        _quanLySinhVienDbContext.MonHocs.Update(mh);
        await _quanLySinhVienDbContext.SaveChangesAsync();

        return Ok(new
        {
            StatusCode = 200,
            Message = "Update mon hoc successfully!",
            Data = new
            {
                IdMonHoc = mh.IdMonHoc,
                TenMonHoc = mh.TenMonHoc,
                IdKhoa = mh.IdKhoa
            }
        });
    }

    /**
     * DELETE: api/monhoc/{id}
     * Delete mon hoc by ID
     */
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMonHoc(string id)
    {
        var monhoc = await _quanLySinhVienDbContext.MonHocs.FindAsync(id);
        if (monhoc == null)
        {
            return NotFound();
        }
        // Kiem tra mon hoc co gan voi lop hoc phan nao khong, neu co khong duoc xoa
        var lophocphans = await _quanLySinhVienDbContext.LopHocPhans.Where(lhp => lhp.IdMonHoc == id).ToListAsync();
        if (lophocphans.Count > 0)
        {
            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Hãy xóa lớp học phần trước!"
            });
        }

        _quanLySinhVienDbContext.MonHocs.Remove(monhoc);
        await _quanLySinhVienDbContext.SaveChangesAsync();

        return Ok(new
        {
            StatusCode = 200,
            Message = "Delete mon hoc successfully!",
            Data = new
            {
                IdMonHoc = monhoc.IdMonHoc,
                TenMonHoc = monhoc.TenMonHoc,
                IdKhoa = monhoc.IdKhoa
            }
        });
    }

    // GET: Get data mon hoc for giao vien with id giao vien
    [HttpGet("giaovien/{IdGiaoVien}")]
    public async Task<IActionResult> GetMonHocFromGiaoVien(string IdGiaoVien)
    {
        var listMH = await (
            from gv in _quanLySinhVienDbContext.GiaoViens
            where gv.IdGiaoVien == IdGiaoVien
            join khoa in _quanLySinhVienDbContext.Khoas on gv.IdKhoa equals khoa.IdKhoa
            join mh in _quanLySinhVienDbContext.MonHocs on khoa.IdKhoa equals mh.IdKhoa
            select new
            {
                IdMonHoc = mh.IdMonHoc,
                TenMonHoc = mh.TenMonHoc,
                TenKhoa = khoa.TenKhoa,
                IdKhoa = khoa.IdKhoa
            }
        ).ToListAsync();

        return Ok(listMH);
    }

}

