using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

//
using web_qlsv.Data;
using web_qlsv.Models;
using web_qlsv.Dto;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiemController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public DiemController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /** 
     * GET: api/diem/
     * Get tat ca diem
     */
    [HttpGet]
    public async Task<IActionResult> GetDiems()
    {
        // Query
        var query = await (
            from d in _context.Diems
            join lhp in _context.LopHocPhans
                on d.IdLopHocPhan equals lhp.IdLopHocPhan
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            select new
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
                TenLopHocPhan = lhp.TenHocPhan,
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/diem/{id}
     * Get diem tu id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiemById(string id)
    {
        // Query
        var query = await (
            from d in _context.Diems
            join lhp in _context.LopHocPhans
                on d.IdLopHocPhan equals lhp.IdLopHocPhan
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            where d.IdDiem == id
            select new
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
                TenLopHocPhan = lhp.TenHocPhan,
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * GET: api/diem/{idSinhVien}/sinhvien
     * Get diem cua sinh vien
     */
    [HttpGet("{idSinhVien}/sinhvien")]
    public async Task<IActionResult> GetDiemFromSinhVien(string idSinhVien)
    {
        // Query
        var query = await (
            from d in _context.Diems
            where d.IdSinhVien == idSinhVien
            join lhp in _context.LopHocPhans
                on d.IdLopHocPhan equals lhp.IdLopHocPhan
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            select new
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
                TenLopHocPhan = lhp.TenHocPhan,
            }
        ).ToListAsync();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * GET: api/diem/{idSinhVien}/sinhvien/dangkinguyenvong
     * Trả Về Danh Sách Điểm Của Sinh Viên Với Lần Thi Cuối Cùng, Có Điểm Tổng Kết >= 7, Chưa Có Trong Nguyện Vọng
     */
    [HttpGet("{idSinhVien}/sinhvien/dangkinguyenvong")]
    public async Task<IActionResult> GetDiemDangKyNguyenVong(string idSinhVien)
    {
        // Lấy các môn học có điểm tổng kết >= 7 và lần học là lớn nhất
        var query = await (
            from diem in _context.Diems
            join lopHocPhan in _context.LopHocPhans
                on diem.IdLopHocPhan equals lopHocPhan.IdLopHocPhan
            join monhoc in _context.MonHocs
                on lopHocPhan.IdMonHoc equals monhoc.IdMonHoc
            where diem.IdSinhVien == idSinhVien && diem.DiemTongKet <= 7
            join latestDiem in (
                from d in _context.Diems
                where d.IdSinhVien == idSinhVien && d.DiemTongKet <= 7
                group d by d.IdLopHocPhan into g
                select new
                {
                    IdLopHocPhan = g.Key,
                    MaxLanHoc = g.Max(x => x.LanHoc)
                }
            ) on new { diem.IdLopHocPhan, diem.LanHoc } equals new { latestDiem.IdLopHocPhan, LanHoc = latestDiem.MaxLanHoc }
            select new
            {
                IdDiem = diem.IdDiem,
                IdSinhVien = diem.IdSinhVien,
                IdLopHocPhan = diem.IdLopHocPhan,
                IdMonHoc = monhoc.IdMonHoc,

                TenLopHocPhan = lopHocPhan.TenHocPhan,
                DiemQuaTrinh = diem.DiemQuaTrinh,
                DiemKetThuc = diem.DiemKetThuc,
                DiemTongKet = diem.DiemTongKet,
                LanHoc = diem.LanHoc,
                TenMonHoc = monhoc.TenMonHoc,
            }
        ).ToListAsync();

        // lay cac mon hoc da dang ky cua sinh vien
        var queryDangKy = await (
            from dk in _context.DangKyNguyenVongs
            where dk.IdSinhVien == idSinhVien
            select dk.IdMonHoc
        ).ToListAsync();

        // Loại bỏ các môn học đã đăng ký
        query = query.Where(x => !queryDangKy.Contains(x.IdMonHoc)).ToList();

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * DELETE: api/diem/{id}
     * Delete diem by id 
     */
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiem(string id)
    {
        // Query
        var query = await (
            from d in _context.Diems
            where d.IdDiem == id
            select d
        ).FirstOrDefaultAsync();

        if (query == null) // Ensure there's data
        {
            return NotFound("Không tìm thấy điểm");
        }

        // Remove the data
        _context.Diems.Remove(query);
        await _context.SaveChangesAsync();

        // Directly return the JSON result
        return Ok("Xóa điểm thành công");
    }

    /**
     * POST: api/diem/
     * Add new diem
     */
    [HttpPost]
    public async Task<IActionResult> AddDiem(DiemDto diem)
    {
        // Handle if dont have Id diem
        if (diem.IdDiem == null)
        {
            diem.IdDiem = Guid.NewGuid().ToString();
        }

        // Check id lop hoc phan, id sinh vien
        var checkLopHocPhan = await _context.LopHocPhans.FindAsync(diem.IdLopHocPhan);
        var checkSinhVien = await _context.SinhViens.FindAsync(diem.IdSinhVien);
        if (checkLopHocPhan == null)
        {
            return BadRequest("Lớp học phần không tồn tại");
        }
        if (checkSinhVien == null)
        {
            return BadRequest("Sinh viên không tồn tại");
        }

        // Create new diem
        var newDiem = new Diem
        {
            IdDiem = diem.IdDiem,
            IdLopHocPhan = diem.IdLopHocPhan,
            IdSinhVien = diem.IdSinhVien,

            DiemQuaTrinh = diem.DiemQuaTrinh ?? 0,
            DiemKetThuc = diem.DiemKetThuc ?? 0,
            DiemTongKet = diem.DiemTongKet ?? 0,
            LanHoc = diem.LanHoc ?? -1,
        };

        _context.Diems.Add(newDiem);
        await _context.SaveChangesAsync();

        return Ok();
    }

    /**
     * PUT: api/diem/{IdDiem}
     * Update diem by id
     */
    [HttpPut("{IdDiem}")]
    public async Task<IActionResult> EditDiem(string IdDiem, DiemDto diem)
    {

        if (diem.IdDiem != IdDiem)
        {
            return BadRequest("Id không khớp");
        }

        // Check id lop hoc phan, id sinh vien
        var checkLopHocPhan = await _context.LopHocPhans.FindAsync(diem.IdLopHocPhan);
        var checkSinhVien = await _context.SinhViens.FindAsync(diem.IdSinhVien);
        if (checkLopHocPhan == null)
        {
            return BadRequest("Lớp học phần không tồn tại");
        }
        if (checkSinhVien == null)
        {
            return BadRequest("Sinh viên không tồn tại");
        }

        // Query
        var query = await (
            from d in _context.Diems
            where d.IdDiem == IdDiem
            select d
        ).FirstOrDefaultAsync();

        if (query == null) // Ensure there's data
        {
            return NotFound("Không tìm thấy điểm");
        }

        // Update the data
        query.IdLopHocPhan = diem.IdLopHocPhan;
        query.IdSinhVien = diem.IdSinhVien;
        query.DiemQuaTrinh = diem.DiemQuaTrinh ?? 0;
        query.DiemKetThuc = diem.DiemKetThuc ?? 0;
        query.DiemTongKet = diem.DiemTongKet ?? 0;
        query.LanHoc = diem.LanHoc ?? -1;

        await _context.SaveChangesAsync();

        return Ok("Cập nhật điểm thành công");
    }

    /**
     * GET: /api/diem/{idLopHocPhan}/lophocphan
     * Get diem by id lop hoc phan
     */
    [HttpGet("{idLopHocPhan}/lophocphan")]
    public async Task<IActionResult> GetDiemByIdLopHocPhan(string idLopHocPhan)
    {
        var qr = await (
            from lhp in _context.LopHocPhans
            where lhp.IdLopHocPhan == idLopHocPhan
            join d in _context.Diems
                on lhp.IdLopHocPhan equals d.IdLopHocPhan
            join mon in _context.MonHocs
                on lhp.IdMonHoc equals mon.IdMonHoc
            join sv in _context.SinhViens
                on d.IdSinhVien equals sv.IdSinhVien
            select new
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,
                IdMon = mon.IdMonHoc,

                TenSinhVien = sv.HoTen,
                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
                TenMonHoc = mon.TenMonHoc,
                TenLopHocPhan = lhp.TenHocPhan,
            }
        ).ToListAsync();

        return Ok(qr);
    }

    /**
     * PUT: /api/diem/nhap/{idDiem}
     * PUT: Nhập điểm cho sinh viên
     */
    [HttpPut("nhap")]
    public async Task<IActionResult> NhapDiemSinhVien(NhapDiemDto nhapDiemDto)
    {
        if (nhapDiemDto.IdDiem == null)
        {
            return BadRequest("Id điểm không được để trống");
        }
        // Query
        var qr = _context.Diems.Where(x => x.IdDiem == nhapDiemDto.IdDiem).FirstOrDefault();
        if (qr == null)
        {
            return NotFound("Không tìm thấy điểm");
        }

        qr.DiemQuaTrinh = nhapDiemDto.DiemQuaTrinh;
        qr.DiemKetThuc = nhapDiemDto.DiemKetThuc;
        qr.DiemTongKet = nhapDiemDto.DiemTongKet;
        _context.Diems.Update(qr);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            StatusCode = 200,
            Message = "Cập Nhập Thành Công"
        });
    }

    /**
     * PUT:  /api/diem/nhaplist/
     * PUT: Cap Nhap List Diem Sinh Vien
     */
    [HttpPut("nhaplist")]
    public async Task<IActionResult> NhapListDiemSinhVien(List<NhapDiemDto> listDiem)
    {
        foreach (NhapDiemDto diem in listDiem)
        {
            if (diem.IdDiem == null)
            {
                return BadRequest("Id điểm không được để trống");
            }
            // Query
            var qr = _context.Diems.Where(x => x.IdDiem == diem.IdDiem).FirstOrDefault();
            if (qr == null)
            {
                return NotFound("Không tìm thấy điểm với id: " + diem.IdDiem);
            }

            qr.DiemQuaTrinh = diem.DiemQuaTrinh;
            qr.DiemKetThuc = diem.DiemKetThuc;
            qr.DiemTongKet = diem.DiemTongKet;
            _context.Diems.Update(qr);
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            StatusCode = 200,
            Message = "Cập Nhập Thành Công List Điểm"
        });
    }
}