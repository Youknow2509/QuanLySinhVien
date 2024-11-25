using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

//
using web_qlsv.Data;
using web_qlsv.Models;

namespace web_qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalendarController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public CalendarController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    // GET: Get calendar from giao vien with id
    [HttpGet("giaovien/{id}")]
    public async Task<IActionResult> GetCalendarGiaoVien(string id)
    {
        // Query database to get events for the teacher
        var events = await (from lhp in _context.LopHocPhans
                            join tglhp in _context.ThoiGianLopHocPhans
                                on lhp.IdLopHocPhan equals tglhp.IdLopHocPhan
                            join tg in _context.ThoiGians
                                on tglhp.IdThoiGian equals tg.IdThoiGian
                            join ph in _context.PhongHocs
                                on tg.IdPhongHoc equals ph.IdPhongHoc
                            where lhp.IdGiaoVien == id
                            select new
                            {
                                Id = tg.IdThoiGian,
                                GroupId = lhp.IdLopHocPhan,
                                IdPhongHoc = ph.IdPhongHoc,

                                Title = lhp.TenHocPhan,
                                Start = tg.NgayBatDau,
                                End = tg.NgayKetThuc,
                                Location = ph.DiaChi,
                                Description = $"Lớp Học Phần: {lhp.TenHocPhan}\nPhòng Học: {ph.TenPhongHoc} tại {ph.DiaChi}\nThời Gian Bắt Đầu: {tg.NgayBatDau}\nThời Gian Kết Thúc: {tg.NgayKetThuc}",
                            }).ToListAsync();

        return Ok(
            events
        );
    }


    // GET: Get calendar from sinh vien with id
    [HttpGet("sinhvien/{id}")]
    public async Task<IActionResult> GetCalendarSinhVien(string id)
    {
        // Query database to get events for the student
        var events = await (from svlhp in _context.SinhVienLopHocPhans
                            join lhp in _context.LopHocPhans
                                on svlhp.IdLopHocPhan equals lhp.IdLopHocPhan
                            join tglhp in _context.ThoiGianLopHocPhans
                                on lhp.IdLopHocPhan equals tglhp.IdLopHocPhan
                            join tg in _context.ThoiGians
                                on tglhp.IdThoiGian equals tg.IdThoiGian
                            join ph in _context.PhongHocs
                                on tg.IdPhongHoc equals ph.IdPhongHoc
                            where svlhp.IdSinhVien == id
                            select new
                            {
                                Id = tg.IdThoiGian,
                                GroupId = lhp.IdLopHocPhan,
                                Title = lhp.TenHocPhan,
                                Start = tg.NgayBatDau,
                                End = tg.NgayKetThuc,
                                Location = ph.DiaChi,
                                Description = $"Lớp Học Phần: {lhp.TenHocPhan}\nPhòng Học: {ph.TenPhongHoc} tại {ph.DiaChi}\nThời Gian Bắt Đầu: {tg.NgayBatDau}\nThời Gian Kết Thúc: {tg.NgayKetThuc}",

                            }).ToListAsync();

        // Add results to listEvent        
        return Ok(
            events
        );
    }

    /**
     * GET: api/calendar/lophocphan/{id}
     * Get calerdar lop hoc phan with id lop hoc pha
     */
    [HttpGet("lophocphan/{id}")]
    public async Task<IActionResult> GetCalendarLopHocPhan(string id)
    {
        var listEvents = await (
            from tg_lhp in _context.ThoiGianLopHocPhans
            where tg_lhp.IdLopHocPhan == id
            join lhp in _context.LopHocPhans
                on tg_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
            join tg in _context.ThoiGians
                on tg_lhp.IdThoiGian equals tg.IdThoiGian
            join ph in _context.PhongHocs
                on tg.IdPhongHoc equals ph.IdPhongHoc
            select new
            {
                Id = tg.IdThoiGian,
                GroupId = tg_lhp.IdLopHocPhan,
                Title = lhp.TenHocPhan,
                Start = tg.NgayBatDau,
                End = tg.NgayKetThuc,
                Location = ph.DiaChi,
                Description = $"Lớp Học Phần: {lhp.TenHocPhan}\nPhòng Học: {ph.TenPhongHoc} tại {ph.DiaChi}\nThời Gian Bắt Đầu: {tg.NgayBatDau}\nThời Gian Kết Thúc: {tg.NgayKetThuc}",
            }
        ).ToListAsync();

        return Ok(listEvents);
    }

}


