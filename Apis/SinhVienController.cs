using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

//
using web_qlsv.Data;
using web_qlsv.Models;
// using web_qlsv.Dto;

namespace web_qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SinhVienController : ControllerBase
{
    // Variables 
    private readonly QuanLySinhVienDbContext _context;

    // Constructor 
    public SinhVienController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /**
     * GET: api/SinhVien/
     * Get all sinh vien 
     */
    [HttpGet]
    public async Task<IActionResult> GetSinhViens()
    {
        var req = await _context.Sinhviens.ToListAsync();
        return Ok(req);
    }
}
