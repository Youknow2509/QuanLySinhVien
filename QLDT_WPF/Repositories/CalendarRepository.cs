using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using QLDT_WPF.Data;
using QLDT_WPF.Models;
using QLDT_WPF.Dto;

namespace QLDT_WPF.Repositories
{
    public class CalendarRepository : IDisposable
    {
        private readonly QuanLySinhVienDbContext _context;

        public CalendarRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["QuanLySinhVienDbConnection"].ConnectionString;
            _context = new QuanLySinhVienDbContext(
                new DbContextOptionsBuilder<QuanLySinhVienDbContext>()
                    .UseSqlServer(connectionString)
                    .Options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        // Get calendar from giao vien with id
        public async Task<ApiResponse<List<CalendarDto>>>
            GetCalendarFromGiaoVien(string id)
        {
            var gv_c = await _context.GiaoViens
                .FirstOrDefaultAsync(gv => gv.IdGiaoVien == id);
            if (gv_c == null)
            {
                return new ApiResponse<List<CalendarDto>>
                {
                    Status = false,
                    Message = "Giáo Viên Không Tồn Tại !!!",
                    Data = null
                };
            }

            // Query database to get events for the teacher
            var events = await (
                from lhp in _context.LopHocPhans
                join tglhp in _context.ThoiGianLopHocPhans on lhp.IdLopHocPhan equals tglhp.IdLopHocPhan
                join tg in _context.ThoiGians on tglhp.IdThoiGian equals tg.IdThoiGian
                where lhp.IdGiaoVien == id
                select new CalendarDto
                {
                    Id = tg.IdThoiGian,
                    GroupId = lhp.IdLopHocPhan,
                    Title = lhp.TenHocPhan,
                    Description = $"Giáo viên: {lhp.TenHocPhan}, Địa Điểm {tg.DiaDiem}",
                    Start = tg.NgayBatDau,
                    End = tg.NgayKetThuc,
                    Location = tg.DiaDiem
                }
            ).ToListAsync();

            return new ApiResponse<List<CalendarDto>>
            {
                Status = true,
                Data = events,
                Message = "Lấy Dữ Liệu Thành Công !!!"
            };
        }

        // GET: Get calendar from sinh vien with id
        public async Task<ApiResponse<List<CalendarDto>>>
            GetCalendarSinhVien(string id)
        {
            var sv_c = await _context.SinhViens
                .FirstOrDefaultAsync(sv => sv.IdSinhVien == id);
            if (sv_c == null)
            {
                return new ApiResponse<List<CalendarDto>>
                {
                    Status = false,
                    Message = "Sinh Viên Không Tồn Tại !!!",
                    Data = null
                };
            }

            // Query database to get events for the student
            var events = await (
                from svlhp in _context.SinhVienLopHocPhans
                join lhp in _context.LopHocPhans on svlhp.IdLopHocPhan equals lhp.IdLopHocPhan
                join tglhp in _context.ThoiGianLopHocPhans on lhp.IdLopHocPhan equals tglhp.IdLopHocPhan
                join tg in _context.ThoiGians on tglhp.IdThoiGian equals tg.IdThoiGian
                where svlhp.IdSinhVien == id
                select new CalendarDto
                {
                    Id = tg.IdThoiGian,
                    GroupId = lhp.IdLopHocPhan,
                    Title = lhp.TenHocPhan,
                    Description = $"Lớp: {lhp.TenHocPhan}, Địa Điểm {tg.DiaDiem}",
                    Start = tg.NgayBatDau,
                    End = tg.NgayKetThuc,
                    Location = tg.DiaDiem
                }).ToListAsync();

            // Add results to listEvent        

            return new ApiResponse<List<CalendarDto>>
            {
                Status = true,
                Data = events,
                Message = "Lấy Dữ Liệu Thành Công !!!"
            };
        }

        // Get calerdar lop hoc phan with id lop hoc phan 
        public async Task<ApiResponse<List<CalendarDto>>>
            GetCalendarLopHocPhan(string id)
        {
            var lhp_c = await _context.LopHocPhans
                .FirstOrDefaultAsync(lhp => lhp.IdLopHocPhan == id);
            if (lhp_c == null)
            {
                return new ApiResponse<List<CalendarDto>>
                {
                    Status = false,
                    Message = "Lớp Học Phần Không Tồn Tại !!!",
                    Data = null
                };
            }

            // Query database to get events for the class
            var events = await (
                from tg_lhp in _context.ThoiGianLopHocPhans
                where tg_lhp.IdLopHocPhan == id
                join lhp in _context.LopHocPhans on tg_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
                join tg in _context.ThoiGians on tg_lhp.IdThoiGian equals tg.IdThoiGian
                select new CalendarDto
                {
                    Id = tg.IdThoiGian,
                    GroupId = tg_lhp.IdLopHocPhan,
                    Title = lhp.TenHocPhan,
                    Description = $"Lớp: {lhp.TenHocPhan}, Địa Điểm {tg.DiaDiem}",
                    Start = tg.NgayBatDau,
                    End = tg.NgayKetThuc,
                    Location = tg.DiaDiem,
                }
            ).ToListAsync();

            return new ApiResponse<List<CalendarDto>>
            {
                Status = true,
                Data = events,
                Message = "Lấy Dữ Liệu Thành Công !!!"
            };
        }
    }
}
