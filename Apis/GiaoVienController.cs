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
    public async Task<IActionResult> CreateGiaoVien([FromBody] GiaoVienDto newGiaoVien)
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
            var existingGiaoVien = await _context.GiaoViens
                .FirstOrDefaultAsync( x => x.IdGiaoVien == giaoVien.IdGiaoVien);
            if (existingGiaoVien != null)
            {
                return BadRequest("ID Giáo Viên Đã Tồn Tại !!!");
            }
            existingGiaoVien = await _context.GiaoViens
                .FirstOrDefaultAsync(x => x.Email == giaoVien.Email);
            if (existingGiaoVien != null){
                return BadRequest("Email Đã Tồn Tại !!!");
            }
            existingGiaoVien = await _context.GiaoViens
                .FirstOrDefaultAsync(x => x.SoDienThoai == giaoVien.SoDienThoai);
            if (existingGiaoVien != null)
            {
                return BadRequest("Số Điện Thoại Đã Tồn Tại !!!");
            }

            _context.GiaoViens.Add(giaoVien);
            await _context.SaveChangesAsync();

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

    // Delete giao vien
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGiaoVien(string id)
    {
        // Find the existing giao vien
        var existingGiaoVien = await _context.GiaoViens.FindAsync(id);
        if (existingGiaoVien == null)
        {
            return NotFound("Không tìm thấy giáo viên");
        }

        // Remove diem 
        var diems = await _context.Diems
            .Where(d => d.IdLopHocPhan == id)
            .ToListAsync();
        if (diems != null){
            _context.Diems.RemoveRange(diems);
        }

        // Remove sinh vien lop hoc phan
        var sinhVienLopHocPhans = await _context.SinhVienLopHocPhans
            .Where(sv => sv.IdLopHocPhan == id)
            .ToListAsync();
        if (sinhVienLopHocPhans != null)    
        {
            _context.SinhVienLopHocPhans.RemoveRange(sinhVienLopHocPhans);
        }

        // Remove thoi gian lop hoc phan
        var thoiGianLopHocPhans = await _context.ThoiGianLopHocPhans
            .Where(tg => tg.IdLopHocPhan == id)
            .ToListAsync();
        if (thoiGianLopHocPhans != null)
        {
            _context.ThoiGianLopHocPhans.RemoveRange(thoiGianLopHocPhans);
        }
        
        // Remove lop hoc phan
        var lopHocPhans = await _context.LopHocPhans
            .Where(lhp => lhp.IdGiaoVien == id)
            .ToListAsync();
        if (lopHocPhans != null)
        {
            _context.LopHocPhans.RemoveRange(lopHocPhans);
        }

        // Remove the giao vien
        _context.GiaoViens.Remove(existingGiaoVien);
        await _context.SaveChangesAsync();

        // Return the deleted giao vien
        return Ok(existingGiaoVien);
    }
}

