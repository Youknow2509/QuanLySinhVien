using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_qlsv.Data;
using web_qlsv.Dto;
using web_qlsv.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace web_qlsv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguyenVongThayDoiLichController : ControllerBase
    {
        private readonly QuanLySinhVienDbContext _context;

        public NguyenVongThayDoiLichController(QuanLySinhVienDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNguyenVong()
        {
            var requests = await (
                from nguyenvong in _context.DangKyDoiLichs
                join tg_lhp in _context.ThoiGianLopHocPhans
                    on nguyenvong.IdThoiGian equals tg_lhp.IdThoiGian
                join lhp in _context.LopHocPhans
                    on tg_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
                select new
                {
                    idDangKyDoiLich = nguyenvong.IdDangKyDoiLich,
                    idThoiGian = nguyenvong.IdThoiGian,
                    thoiGianBatDauHienTai = nguyenvong.ThoiGianBatDauHienTai,
                    thoiGianKetThucHienTai = nguyenvong.ThoiGianKetThucHienTai,
                    thoiGianBatDauMoi = nguyenvong.ThoiGianBatDauMoi,
                    thoiGianKetThucMoi = nguyenvong.ThoiGianKetThucMoi,
                    trangThai = nguyenvong.TrangThai,
                    idLopHocPhan = lhp.IdLopHocPhan,
                    tenLopHocPhan = lhp.TenHocPhan,
                }
            ).ToListAsync();

            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNguyenVongById(string id)
        {
            var request = await (
                from nguyenvong in _context.DangKyDoiLichs
                join tg_lhp in _context.ThoiGianLopHocPhans
                    on nguyenvong.IdThoiGian equals tg_lhp.IdThoiGian
                join lhp in _context.LopHocPhans
                    on tg_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
                where nguyenvong.IdDangKyDoiLich == id
                select new
                {
                    idDangKyDoiLich = nguyenvong.IdDangKyDoiLich,
                    idThoiGian = nguyenvong.IdThoiGian,
                    thoiGianBatDauHienTai = nguyenvong.ThoiGianBatDauHienTai,
                    thoiGianKetThucHienTai = nguyenvong.ThoiGianKetThucHienTai,
                    thoiGianBatDauMoi = nguyenvong.ThoiGianBatDauMoi,
                    thoiGianKetThucMoi = nguyenvong.ThoiGianKetThucMoi,
                    trangThai = nguyenvong.TrangThai,
                    idLopHocPhan = lhp.IdLopHocPhan,
                    tenHocPhan = lhp.TenHocPhan
                }
            ).FirstOrDefaultAsync();

            if (request == null)
            {
                return NotFound("Nguyện Vọng Không Tìm Thấy");
            }

            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNguyenVong([FromBody] NguyenVongThayDoiLichDto nguyenVong)
        {
            if (await CheckIfAlreadyRegistered(nguyenVong.IdThoiGian))
                return BadRequest("Nguyện Vọng Đã Được Đăng Ký !!!");

            if (!IsValidTimeRange(nguyenVong.ThoiGianBatDauHienTai, nguyenVong.ThoiGianKetThucHienTai) ||
                !IsValidTimeRange(nguyenVong.ThoiGianBatDauMoi, nguyenVong.ThoiGianKetThucMoi))
                return BadRequest("Thời Gian Bắt Đầu Lớn Hơn Thời Gian Kết Thúc");

            if (nguyenVong.ThoiGianBatDauMoi < DateTime.Now)
                return BadRequest("Thời gian truyền vào ở quá khứ");

            if (await CheckIfTimeOverlap(nguyenVong.IdThoiGian, nguyenVong.ThoiGianBatDauMoi, nguyenVong.ThoiGianKetThucMoi))
                return BadRequest("Thời gian bị trùng lịch hiện tại");

            var newRequest = new DangKyDoiLich
            {
                IdDangKyDoiLich = Guid.NewGuid().ToString(),
                IdThoiGian = nguyenVong.IdThoiGian,
                ThoiGianBatDauHienTai = nguyenVong.ThoiGianBatDauHienTai,
                ThoiGianKetThucHienTai = nguyenVong.ThoiGianKetThucHienTai,
                ThoiGianBatDauMoi = nguyenVong.ThoiGianBatDauMoi,
                ThoiGianKetThucMoi = nguyenVong.ThoiGianKetThucMoi,
                TrangThai = -1
            };

            _context.DangKyDoiLichs.Add(newRequest);
            await _context.SaveChangesAsync();

            return Ok(new { statusCode = 200, message = "Tạo Nguyện Vọng Thành Công" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNguyenVong([FromBody] NguyenVongThayDoiLichDto nguyenVong)
        {
            var existingRequest = await _context.DangKyDoiLichs
                .FirstOrDefaultAsync(nv => nv.IdDangKyDoiLich == nguyenVong.IdDangKyDoiLich);

            if (existingRequest == null)
                return NotFound("Không Tìm Thấy Nguyện Vọng");

            if (!IsValidTimeRange(nguyenVong.ThoiGianBatDauHienTai, nguyenVong.ThoiGianKetThucHienTai) ||
                !IsValidTimeRange(nguyenVong.ThoiGianBatDauMoi, nguyenVong.ThoiGianKetThucMoi))
                return BadRequest("Thời Gian Bắt Đầu Lớn Hơn Thời Gian Kết Thúc");

            if (nguyenVong.ThoiGianBatDauMoi < DateTime.Now)
                return BadRequest("Thời gian truyền vào ở quá khứ");

            if (await CheckIfTimeOverlap(nguyenVong.IdThoiGian, nguyenVong.ThoiGianBatDauMoi, nguyenVong.ThoiGianKetThucMoi))
                return BadRequest("Thời gian bị trùng lịch hiện tại");

            existingRequest.ThoiGianBatDauHienTai = nguyenVong.ThoiGianBatDauHienTai;
            existingRequest.ThoiGianKetThucHienTai = nguyenVong.ThoiGianKetThucHienTai;
            existingRequest.ThoiGianBatDauMoi = nguyenVong.ThoiGianBatDauMoi;
            existingRequest.ThoiGianKetThucMoi = nguyenVong.ThoiGianKetThucMoi;

            await _context.SaveChangesAsync();

            return Ok("Cập Nhật Nguyện Vọng Thành Công");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNguyenVong(string id)
        {
            var request = await _context.DangKyDoiLichs
                .FirstOrDefaultAsync(nv => nv.IdDangKyDoiLich == id);

            if (request == null)
                return NotFound("Không Tìm Thấy Nguyện Vọng");

            _context.DangKyDoiLichs.Remove(request);
            await _context.SaveChangesAsync();

            return Ok("Xoá Nguyện Vọng Thành Công");
        }

        // Chấp Nhận Yêu Cầu
        [HttpPut("{id}/accpet")]
        public async Task<IActionResult> GetAccpet(string id)
        {

            var qr = await _context.DangKyDoiLichs
                .FirstOrDefaultAsync(q => q.IdDangKyDoiLich == id);

            if (qr == null)
            {
                return BadRequest("Không Tìm Thấy Lịch Đăng Ký");
            }

            qr.TrangThai = 1;

            await _context.SaveChangesAsync();

            return Ok("Chấp Nhận Đăng Ký Lịch Giáo Viên " + id);
        }

        // Từ Chối Yêu Cầu
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectRequest(string id)
        {
            var request = await _context.DangKyDoiLichs
                .FirstOrDefaultAsync(q => q.IdDangKyDoiLich == id);

            if (request == null)
            {
                return BadRequest("Không Tìm Thấy Lịch Đăng Ký");
            }

            // Set TrangThai to -1 to indicate rejection
            request.TrangThai = 0;

            await _context.SaveChangesAsync();

            return Ok("Đã Từ Chối Đăng Ký Lịch Giáo Viên " + id);
        }


        // Find Nguyen Vong Cua Giao Vien
        [HttpGet("{idGiaoVien}/giaovien")]
        public async Task<IActionResult> GetNguyenVongGiaoVien(string idGiaoVien)
        {
            var dangKyDoiLichList = await (from dkdl in _context.DangKyDoiLichs
                                           join tglhp in _context.ThoiGianLopHocPhans on dkdl.IdThoiGian equals tglhp.IdThoiGian
                                           join lhp in _context.LopHocPhans on tglhp.IdLopHocPhan equals lhp.IdLopHocPhan
                                           where lhp.IdGiaoVien == idGiaoVien
                                           select new
                                           {
                                               dkdl.IdDangKyDoiLich,
                                               dkdl.ThoiGianBatDauHienTai,
                                               dkdl.ThoiGianKetThucHienTai,
                                               dkdl.ThoiGianBatDauMoi,
                                               dkdl.ThoiGianKetThucMoi,
                                               dkdl.TrangThai,
                                               lhp.TenHocPhan
                                           }).ToListAsync();


            return Ok(dangKyDoiLichList);
        }

        // Helper method to check if already registered
        private async Task<bool> CheckIfAlreadyRegistered(string idThoiGian) =>
            await _context.DangKyDoiLichs.AnyAsync(t => t.IdThoiGian == idThoiGian);

        // Helper method to validate time range
        private bool IsValidTimeRange(DateTime start, DateTime end) => start < end;

        // Helper method to check for overlapping times
        private async Task<bool> CheckIfTimeOverlap(string idThoiGian, DateTime start, DateTime end)
        {
            var lhp = await _context.ThoiGianLopHocPhans
                .Where(l => l.IdThoiGian == idThoiGian)
                .Select(l => l.IdLopHocPhan)
                .FirstOrDefaultAsync();

            if (lhp == null) return true;

            return await (from tl in _context.ThoiGianLopHocPhans
                          join tgCheck in _context.ThoiGians on tl.IdThoiGian equals tgCheck.IdThoiGian
                          where tl.IdLopHocPhan == lhp &&
                                ((start < tgCheck.NgayKetThuc && start > tgCheck.NgayBatDau) ||
                                 (end < tgCheck.NgayKetThuc && end > tgCheck.NgayBatDau))
                          select tgCheck).AnyAsync();
        }
    }
}
