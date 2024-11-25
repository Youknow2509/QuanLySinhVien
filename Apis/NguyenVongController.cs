using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

//
using web_qlsv.Data;
using web_qlsv.Models;
using web_qlsv.Dto;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NguyenVongController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public NguyenVongController(
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _context = quanLySinhVienDbContext;
    }

    /**
     * GET: api/nguyenvong/
     * Get all nguyen vong 
     */
    [HttpGet]
    public async Task<IActionResult> GetNguyenVongs()
    {
        var nguyenVongs = (
            from nv in _context.DangKyNguyenVongs
            join sv in _context.SinhViens on nv.IdSinhVien equals sv.IdSinhVien
            join mh in _context.MonHocs on nv.IdMonHoc equals mh.IdMonHoc
            select new
            {
                IdDangKyNguyenVong = nv.IdDangKyNguyenVong,
                IdSinhVien = nv.IdSinhVien,
                IdMonHoc = mh.IdMonHoc,
                TenSinhVien = sv.HoTen,
                TenMonHoc = mh.TenMonHoc,
                TrangThai = nv.TrangThai,
            }
        ).ToList();

        return Ok(nguyenVongs);
    }

    /**
     * GET: api/nguyenvong/{id}/
     * get nguyen vong by id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNguyenVongById(string id)
    {
        var nguyenVongs = (
            from nv in _context.DangKyNguyenVongs
            where nv.IdDangKyNguyenVong == id
            join sv in _context.SinhViens on nv.IdSinhVien equals sv.IdSinhVien
            join mh in _context.MonHocs on nv.IdMonHoc equals mh.IdMonHoc
            select new
            {
                IdDangKyNguyenVong = nv.IdDangKyNguyenVong,
                IdSinhVien = nv.IdSinhVien,
                IdMonHoc = mh.IdMonHoc,
                TenSinhVien = sv.HoTen,
                TenMonHoc = mh.TenMonHoc,
                TrangThai = nv.TrangThai,
            }
        ).ToList();

        return Ok(nguyenVongs);
    }

    /**
     * GET: api/nguyenvong/sinhvien/{id}/
     * get nguyen vong by sinh vien id
     */
    [HttpGet("sinhvien/{id}")]
    public async Task<IActionResult> GetNguyenVongBySinhVien(string id)
    {
        var nguyenVongs = (
            from nv in _context.DangKyNguyenVongs
            where nv.IdSinhVien == id
            join sv in _context.SinhViens on nv.IdSinhVien equals sv.IdSinhVien
            join mh in _context.MonHocs on nv.IdMonHoc equals mh.IdMonHoc
            select new
            {
                IdDangKyNguyenVong = nv.IdDangKyNguyenVong,
                IdSinhVien = nv.IdSinhVien,
                IdMonHoc = mh.IdMonHoc,
                TenSinhVien = sv.HoTen,
                TenMonHoc = mh.TenMonHoc,
                TrangThai = nv.TrangThai,
            }
        ).ToList();

        return Ok(nguyenVongs);
    }

    /**
     * POST: api/nguyenvong/
     * create new nguyen vong
     */
    [HttpPost]
    public async Task<IActionResult> CreateNguyenVong(NguyenVongSinhVienDto nguyenVong)
    {
        // Check id sinh vien, id mon hoc
        var checkSinhVien = await _context.SinhViens.FindAsync(nguyenVong.IdSinhVien);
        var checkMonHoc = await _context.MonHocs.FindAsync(nguyenVong.IdMonHoc);
        if (checkSinhVien == null || checkMonHoc == null)
        {
            return NotFound("Không tìm thấy sinh viên hoặc môn học");
        }

        // Check xem nguyen vong da dang ky chua, neu roi khong cho dang ki tiep
        var checkNguyenVong = _context.DangKyNguyenVongs
            .Where(nv => nv.IdSinhVien == nguyenVong.IdSinhVien && nv.IdMonHoc == nguyenVong.IdMonHoc)
            .FirstOrDefault();
        if (checkNguyenVong != null)
        {
            return BadRequest("Nguyện vọng đã được đăng ký");
        }

        // Create new nguyen vong
        DangKyNguyenVong dangKyNguyenVong = new DangKyNguyenVong
        {
            IdSinhVien = nguyenVong.IdSinhVien,
            IdMonHoc = nguyenVong.IdMonHoc,
        };

        _context.DangKyNguyenVongs.Add(dangKyNguyenVong);
        await _context.SaveChangesAsync();

        return Ok("Nguyện vọng đã được thêm");
    }

    /**
     * PUT: api/nguyenvong/{id}
     * update nguyen vong by id
     */
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNguyenVong(string id, DangKyNguyenVong nguyenVong)
    {
        var existingNguyenVong = await _context.DangKyNguyenVongs.FindAsync(id);
        if (existingNguyenVong == null)
        {
            return NotFound("Không tìm thấy nguyện vọng");
        }

        // Update the existing nguyen vong
        existingNguyenVong.IdSinhVien = nguyenVong.IdSinhVien;
        existingNguyenVong.IdMonHoc = nguyenVong.IdMonHoc;
        existingNguyenVong.TrangThai = nguyenVong.TrangThai;

        await _context.SaveChangesAsync();

        return Ok(existingNguyenVong);
    }

    /**
     * DELETE: api/nguyenvong/{id}
     * delete nguyen vong by id
     */
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNguyenVong(string id)
    {
        var nguyenVong = await _context.DangKyNguyenVongs.FindAsync(id);
        if (nguyenVong == null)
        {
            return NotFound("Không tìm thấy nguyện vọng");
        }

        _context.DangKyNguyenVongs.Remove(nguyenVong);
        await _context.SaveChangesAsync();

        return Ok("Nguyện vọng đã xóa");
    }

    /**
     * PUT: /api/nguyenvong/{id}/approve
     * PUT: Chap nhan
     */
    [HttpPut("{id}/approve")]
    public async Task<IActionResult> ApproveNguyenVong(string id)
    {
        var nguyenVong = await _context.DangKyNguyenVongs.FindAsync(id);
        if (nguyenVong == null)
        {
            return NotFound("Không tìm thấy nguyện vọng");
        }

        nguyenVong.TrangThai = 1;
        await _context.SaveChangesAsync();

        return Ok(nguyenVong);
    }

    /**
     * PUT: /api/nguyenvong/{id}/reject
     * PUT: Tu choi
     */
    [HttpPut("{id}/reject")]
    public async Task<IActionResult> RejectNguyenVong(string id)
    {
        var nguyenVong = await _context.DangKyNguyenVongs.FindAsync(id);
        if (nguyenVong == null)
        {
            return NotFound("Không tìm thấy nguyện vọng");
        }

        nguyenVong.TrangThai = 0;
        await _context.SaveChangesAsync();

        return Ok(nguyenVong);
    }
}

