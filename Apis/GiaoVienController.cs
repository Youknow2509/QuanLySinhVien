using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

//
using web_qlsv.Data;
using web_qlsv.Models;
using web_qlsv.Dto;

namespace web_qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GiaoVienController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public GiaoVienController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /** 
     * GET: api/giaovien/
     * Get tat ca giao vien
     */
    [HttpGet]
    public async Task<IActionResult> GetGiaoViens()
    {
        // Query
        var query = await (
            from gv in _context.GiaoViens
            join k in _context.Khoas on gv.IdKhoa equals k.IdKhoa
            select new
            {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                Email = gv.Email,
                SoDienThoai = gv.SoDienThoai,
                IdKhoa = gv.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/giaovien/{id}
     * Get giao vien tu id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGiaoVienById(string id)
    {
        // Query
        var query = await (
            from gv in _context.GiaoViens
            where gv.IdGiaoVien == id
            join k in _context.Khoas on gv.IdKhoa equals k.IdKhoa
            select new
            {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                Email = gv.Email,
                SoDienThoai = gv.SoDienThoai,
                IdKhoa = gv.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    // POST: api/giaovien/
    [HttpPost]
    public IActionResult CreateGiaoVien([FromBody] GiaoVienDto newGiaoVien)
    {
        if (newGiaoVien == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            // Convert the DTO to the entity model, assuming your entity model is GiaoVien
            var giaoVien = new GiaoVien
            {
                IdGiaoVien = newGiaoVien.IdGiaoVien,
                TenGiaoVien = newGiaoVien.TenGiaoVien,
                Email = newGiaoVien.Email,
                SoDienThoai = newGiaoVien.SoDienThoai,
                IdKhoa = newGiaoVien.IdKhoa
            };
            // Check duplicate ID, email, phone number
            if (_context.GiaoViens.Any(gv => gv.IdGiaoVien == giaoVien.IdGiaoVien))
            {
                return BadRequest("ID giáo viên đã tồn tại.");
            }
            if (_context.GiaoViens.Any(gv => gv.Email == giaoVien.Email))
            {
                return BadRequest("Email đã tồn tại.");
            }
            if (_context.GiaoViens.Any(gv => gv.SoDienThoai == giaoVien.SoDienThoai))
            {
                return BadRequest("Số điện thoại đã tồn tại.");
            }
            _context.GiaoViens.Add(giaoVien);
            _context.SaveChanges();

            return Ok(giaoVien); // Return success with the newly added teacher's data
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // PUT /api/giaovien/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGiaoVien(string id, [FromBody] GiaoVienDto giaoVien)
    {
        // Find the existing giao vien
        var existingGiaoVien = await _context.GiaoViens.FindAsync(id);
        if (existingGiaoVien == null)
        {
            return NotFound("Không tìm thấy giáo viên");
        }

        // Update the existing giao vien
        existingGiaoVien.TenGiaoVien = giaoVien.TenGiaoVien;
        existingGiaoVien.Email = giaoVien.Email;
        existingGiaoVien.SoDienThoai = giaoVien.SoDienThoai;
        existingGiaoVien.IdKhoa = giaoVien.IdKhoa;

        // Save changes
        await _context.SaveChangesAsync();

        // Return the updated giao vien
        return Ok(existingGiaoVien);
    }
}

