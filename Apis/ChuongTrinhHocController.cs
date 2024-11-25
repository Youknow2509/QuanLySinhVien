using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

//
using web_qlsv.Data;
using web_qlsv.Models;
using Microsoft.EntityFrameworkCore;

namespace web_qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChuongTrinhHocController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    // Constructor
    public ChuongTrinhHocController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /** 
     * GET: api/chuongtrinhhoc/
     * Get tat ca chuong trinh hoc
     */
    [HttpGet]
    public async Task<IActionResult> GetChuongTrinhHocs()
    {
        // Query
        var query = await (
            from chh in _context.ChuongTrinhHocs
            select new
            {
                IdChuongTrinhHoc = chh.IdChuongTrinhHoc,
                TenChuongTrinhHoc = chh.TenChuongTrinhHoc
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy chương trình học");
        }

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/chuongtrinhhoc/sinhvien/{id}
     * Get chuong trinh hoc cua sinh vien tu id
     */
    [HttpGet("sinhvien/{id}")]
    public async Task<IActionResult> GetchuongtrinhhocBySinhVien(string id)
    {
        // Query
        var query = await (
            from sv in _context.SinhViens
            where sv.IdSinhVien == id
            join cch in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
            select new
            {
                IdChuongTrinhHoc = cch.IdChuongTrinhHoc,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy chương trình học");
        }

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * GET: /api/chuongtrinhhoc/details/{IdChuongTrinhhoc}
     * Lay ra danh sach chuong trinh hoc dang ton tai, va khong co
     */
    [HttpGet("details/{idChuongtrinhHoc}")]
    public async Task<IActionResult> GetChuongTrinhHocDetails(string idChuongtrinhHoc)
    {
        // Tim cac mon hoc dang co trong chuong trinh hoc
        var monHocInChuongTrinhHoc = await (
            from cch_mh in _context.ChuongTrinhHocMonHocs
            where cch_mh.IdChuongTrinhHoc == idChuongtrinhHoc
            join mh in _context.MonHocs on cch_mh.IdMonHoc equals mh.IdMonHoc
            select new
            {
                IdMonHoc = mh.IdMonHoc,
                IdChuongTrinhHoc = cch_mh.IdChuongTrinhHoc,

                TenMonHoc = mh.TenMonHoc,
            }
        ).ToListAsync();

        // Tim cac mon hoc khong co trong chuong trinh hoc
        var monHocKhongCo = await (
            from mh in _context.MonHocs
            where !(
                from cch_mh in _context.ChuongTrinhHocMonHocs
                where cch_mh.IdChuongTrinhHoc == idChuongtrinhHoc
                select cch_mh.IdMonHoc
            ).Contains(mh.IdMonHoc)
            select new
            {
                IdMonHoc = mh.IdMonHoc,
                IdChuongTrinhHoc = idChuongtrinhHoc,

                TenMonHoc = mh.TenMonHoc,
            }
        ).ToListAsync();

        return Ok(
            new
            {
                monHocInChuongTrinhHoc,
                monHocKhongCo
            }
        );
    }

    /**
     * DELETE: /api/chuongtrinhhoc/delete/{IdMonHoc}/from/{IdChuongTrinhHoc}
     * Xoa mon hoc khoi chuong trinh hoc
     */
    [HttpDelete("delete/{idMonHoc}/from/{idChuongTrinhHoc}")]
    public async Task<IActionResult> DeleteMonHocFromChuongTrinhHoc(string idMonHoc, string idChuongTrinhHoc)
    {
        var mh_cch = await (
            from cch_mh in _context.ChuongTrinhHocMonHocs
            where cch_mh.IdMonHoc == idMonHoc && cch_mh.IdChuongTrinhHoc == idChuongTrinhHoc
            select cch_mh
        ).FirstOrDefaultAsync();

        if (mh_cch == null) // Mon hoc nay khong co trong chuong trinh hoc nay
        {
            return NotFound("Không tìm thấy môn học trong chương trình học");
        }

        _context.ChuongTrinhHocMonHocs.Remove(mh_cch);
        _context.SaveChanges();

        return Ok();
    }

    /**
     * PUT: /api/chuongtrinhhoc/add/{idMonHoc}/to/{idChuongTrinhHoc}
     * Them mon hoc vao chuong trinh hoc
     */
    [HttpPut("add/{idMonHoc}/to/{idChuongTrinhHoc}")]
    public async Task<IActionResult> AddMonHocToChuongTrinhHoc(string idMonHoc, string idChuongTrinhHoc)
    {
        var mh_cch = (
            from cch_mh in _context.ChuongTrinhHocMonHocs
            where cch_mh.IdMonHoc == idMonHoc && cch_mh.IdChuongTrinhHoc == idChuongTrinhHoc
            select cch_mh
        ).FirstOrDefault();

        if (mh_cch != null) // Mon hoc nay da co trong chuong trinh hoc
        {
            return BadRequest("Môn học đã có trong chương trình học");
        }

        _context.ChuongTrinhHocMonHocs.Add(
            new ChuongTrinhHocMonHoc
            {
                IdChuongTrinhHocMonHoc = Guid.NewGuid().ToString(),
                IdMonHoc = idMonHoc,
                IdChuongTrinhHoc = idChuongTrinhHoc
            }
        );
        _context.SaveChanges();

        return Ok(
            new
            {
                message = "Thêm môn học thành công"
            }
        );
    }

    /**
     * POST: /api/chuongtrinhhoc/create
     * Tao chuong trinh hoc moi
     */
    [HttpPost("create")]
    public async Task<IActionResult> CreateChuongTrinhHoc(string TenChuongTrinhHoc)
    {
        _context.ChuongTrinhHocs.Add(
            new ChuongTrinhHoc
            {
                IdChuongTrinhHoc = Guid.NewGuid().ToString(),
                TenChuongTrinhHoc = TenChuongTrinhHoc
            }
        );

        await _context.SaveChangesAsync();

        return Ok(
            new
            {
                message = "Tạo chương trình học thành công"
            }
        );
    }

    /**
     * DELETE: /api/chuongtrinhhoc/delete/{idChuongTrinhHoc}
     * Xoa chuong trinh hoc
     */
    [HttpDelete("delete/{idChuongTrinhHoc}")]
    public async Task<IActionResult> DeleteChuongTrinhHoc(string idChuongTrinhHoc)
    {   
        // Xoa sinh vine khoi chuong trinh hoc 
        // TODO

        // Xoa mon hoc thuoc chuong trinh hoc
        var monHocInChuongTrinhHoc = await (
            from cch_mh in _context.ChuongTrinhHocMonHocs
            where cch_mh.IdChuongTrinhHoc == idChuongTrinhHoc
            select cch_mh
        ).ToListAsync();

        _context.ChuongTrinhHocMonHocs.RemoveRange(monHocInChuongTrinhHoc);
        // Xoa chuong trinh hoc
        var cth = await (
            from chh in _context.ChuongTrinhHocs
            where chh.IdChuongTrinhHoc == idChuongTrinhHoc
            select chh
        ).FirstOrDefaultAsync();

        if (cth == null) // Chuong trinh hoc nay khong ton tai
        {
            return NotFound("Không tìm thấy chương trình học");
        }

        _context.ChuongTrinhHocs.Remove(cth);

        _context.SaveChanges();

        return Ok(
            new
            {
                message = "Xóa chương trình học " + idChuongTrinhHoc + " thành công"
            }
        );
    }

    /**
     * PUT: /api/chuongtrinhhoc/update/{idChuongTrinhHoc}
     * Cap nhat ten chuong trinh hoc
     */
    [HttpPut("update/{idChuongTrinhHoc}")]
    public async Task<IActionResult> UpdateChuongTrinhHoc(string idChuongTrinhHoc, string TenChuongTrinhHoc)
    {
        var cth = await (
            from chh in _context.ChuongTrinhHocs
            where chh.IdChuongTrinhHoc == idChuongTrinhHoc
            select chh
        ).FirstOrDefaultAsync();

        if (cth == null) // Chuong trinh hoc nay khong ton tai
        {
            return NotFound("Không tìm thấy chương trình học");
        }

        cth.TenChuongTrinhHoc = TenChuongTrinhHoc;

        _context.SaveChanges();

        return Ok(
            new
            {
                message = "Cập nhật chương trình học thành công"
            }
        );
    }
}
