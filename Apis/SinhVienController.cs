using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

//
using web_qlsv.Data;
using web_qlsv.Models;
using web_qlsv.Dto;

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
 * GET: api/sinhvien/
 * get all sinh vien
 */
    [HttpGet]
    public async Task<IActionResult> GetSinhViens()
    {
        var sinhViens = (
            from sv in _context.SinhViens
            join khoa in _context.Khoas on sv.IdKhoa equals khoa.IdKhoa
            join cch in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
            select new
            {
                // List id
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                // list value
                TenSinhVien = sv.HoTen,
                TenKhoa = khoa.TenKhoa,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh,
                DiaChi = sv.DiaChi,
            }
        ).ToList();
        return Ok(sinhViens);
    }

    /**
     * GET: api/sinhvien/{id}/
     * get sinh vien by id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSinhVienById(string id)
    {
        var sinhVien = (
            from sv in _context.SinhViens
            where sv.IdSinhVien == id
            join khoa in _context.Khoas on sv.IdKhoa equals khoa.IdKhoa
            join cch in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
            select new
            {
                // List id
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                // list value
                TenSinhVien = sv.HoTen,
                TenKhoa = khoa.TenKhoa,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh,
                DiaChi = sv.DiaChi,
            }
        ).FirstOrDefault();
        return Ok(sinhVien);
    }

    /**
     * PUT: api/sinhvien/{id}/
     * sua thong tin sinh vien
     */
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSinhVien(string id, [FromBody] SinhVienDto sinhVien)
    {
        // Find the existing sinh vien
        var existingSinhVien = await _context.SinhViens.FirstOrDefaultAsync(
            x => x.IdSinhVien == sinhVien.IdSinhVien
        );

        if (existingSinhVien == null)
        {
            return NotFound("Không tìm thấy sinh viên");
        }

        // Update the existing sinh vien
        existingSinhVien.HoTen = sinhVien.HoTen;
        existingSinhVien.Lop = sinhVien.Lop;
        existingSinhVien.NgaySinh = sinhVien.NgaySinh ?? DateTime.MinValue;
        existingSinhVien.DiaChi = sinhVien.DiaChi;

        // Save the changes
        await _context.SaveChangesAsync();
        return Ok(new
        {
            statusCode = 200,
            message = "Cập nhật sinh viên thành công",
            data = new
            {
                IdSinhVien = existingSinhVien.IdSinhVien,

                TenSinhVien = existingSinhVien.HoTen,
                Lop = existingSinhVien.Lop,
                NgaySinh = existingSinhVien.NgaySinh,
                DiaChi = existingSinhVien.DiaChi,
            }
        });
    }

    /**
     * POST: api/sinhvien/
     * them sinh vien
     */
    [HttpPost]
    public async Task<IActionResult> CreateSinhVien([FromBody] SinhVienDto sinhVien)
    {
        if (string.IsNullOrEmpty(sinhVien.IdSinhVien))
        {
            sinhVien.IdSinhVien = Guid.NewGuid().ToString();
        }
        else
        {
            var existingSinhVien = await _context.SinhViens.FirstOrDefaultAsync(
                x => x.IdSinhVien == sinhVien.IdSinhVien
            );

            if (existingSinhVien != null)
            {
                return BadRequest("Sinh viên đã tồn tại");
            }
        }

        SinhVien newSinhVien = new SinhVien
        {
            IdSinhVien = sinhVien.IdSinhVien,
            IdKhoa = sinhVien.IdKhoa,
            IdChuongTrinhHoc = sinhVien.IdChuongTrinhHoc,

            HoTen = sinhVien.HoTen,
            Lop = sinhVien.Lop,
            NgaySinh = sinhVien.NgaySinh ?? DateTime.MinValue,
            DiaChi = sinhVien.DiaChi,
        };

        // check khoa, chuong trinh hoc
        var khoa = await _context.Khoas
            .FirstOrDefaultAsync(x => x.IdKhoa == sinhVien.IdKhoa);
        if (khoa == null)
        {
            return BadRequest("Không tìm thấy khoa");
        }
        var cch = await _context.ChuongTrinhHocs
            .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == sinhVien.IdChuongTrinhHoc);
        if (cch == null)
        {
            return BadRequest("Không tìm thấy chương trình học");
        }

        _context.SinhViens.Add(newSinhVien);

        await _context.SaveChangesAsync();
        return Ok("Thêm sinh viên thành công");
    }

    // DELETE: api/sinhvien/{id}
    // delete sinh vien by id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinhVien(string id)
    {
        var sinhVien = await _context.SinhViens.FindAsync(id);
        if (sinhVien == null)
        {
            return NotFound("Không tìm thấy sinh viên");
        }

        // Xoa diem sinh vien
        var list_diem = await _context.Diems
            .Where(x => x.IdSinhVien == id)
            .ToListAsync();
        if (list_diem != null)
        {
            _context.Diems.RemoveRange(list_diem);
        }

        // Xoa sinh vien lop hoc phan
        var list_sv_lhp = await _context.SinhVienLopHocPhans
            .Where(x => x.IdSinhVien == id)
            .ToListAsync();
        if (list_sv_lhp != null)
        {
            _context.SinhVienLopHocPhans.RemoveRange(list_sv_lhp);
        }
        // Xoa nguyen vong sinh vien
        var list_nguyen_vong = await _context.DangKyNguyenVongs
            .Where(x => x.IdSinhVien == id)
            .ToListAsync();
        if (list_nguyen_vong != null)
        {
            _context.DangKyNguyenVongs.RemoveRange(list_nguyen_vong);
        }
        // Xoa sinh vien
        _context.SinhViens.Remove(sinhVien);

        await _context.SaveChangesAsync();

        return Ok("Xóa sinh viên thành công");
    }

    /**
     * GET: api/sinhvien/{id}/lophocphan
     * GET Sinh Vien Thuoc Lop Hoc Phan
     */
    [HttpGet("{id}/lophocphan")]
    public async Task<IActionResult> GetSinhVienThuocLopHocPhan(string id)
    {
        var res = await (
            from lhp in _context.LopHocPhans
            join svlhp in _context.SinhVienLopHocPhans on lhp.IdLopHocPhan equals svlhp.IdLopHocPhan
            join sv in _context.SinhViens on svlhp.IdSinhVien equals sv.IdSinhVien
            join k in _context.Khoas on sv.IdKhoa equals k.IdKhoa
            join cth in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cth.IdChuongTrinhHoc
            where lhp.IdLopHocPhan == id
            select new
            {
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,

                TenSinhVien = sv.HoTen,
                TenKhoa = k.TenKhoa,
                TenChuongTrinhHoc = cth.TenChuongTrinhHoc,

                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh,
                DiaChi = sv.DiaChi,
            }
        ).ToListAsync();

        return Ok(res);
    }

}
