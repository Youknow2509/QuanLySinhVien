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
    public class ChuongTrinhHocRepository : IDisposable
    {
        private readonly QuanLySinhVienDbContext _context;

        public ChuongTrinhHocRepository()
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

        /**
         * Lấy tất cả chương trình học
         */
        public async Task<ApiResponse<List<ChuongTrinhHocDto>>> GetAll()
        {
            var list_cth = await _context.ChuongTrinhHocs
                .Select(x => new ChuongTrinhHocDto
                {
                    IdChuongTrinhHoc = x.IdChuongTrinhHoc,
                    TenChuongTrinhHoc = x.TenChuongTrinhHoc
                })
                .ToListAsync();

            return new ApiResponse<List<ChuongTrinhHocDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Lấy danh sách chương trình học thành công.",
                Data = list_cth
            };
        }

        /**
         * Lấy chương trình học theo ID
         */
        public async Task<ApiResponse<ChuongTrinhHocDto>> GetById(string id)
        {
            var cth = await _context.ChuongTrinhHocs
                .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == id);

            if (cth == null)
            {
                return new ApiResponse<ChuongTrinhHocDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Không tìm thấy chương trình học.",
                    Data = null
                };
            }

            return new ApiResponse<ChuongTrinhHocDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Lấy chương trình học thành công.",
                Data = new ChuongTrinhHocDto
                {
                    IdChuongTrinhHoc = cth.IdChuongTrinhHoc,
                    TenChuongTrinhHoc = cth.TenChuongTrinhHoc
                }
            };
        }

        /**
         * Lấy chương trình học theo ID sinh viên
         */
        public async Task<ApiResponse<List<ChuongTrinhHocDto>>> GetByIdSinhVien(string id)
        {
            var query = await (
                from sv in _context.SinhViens
                where sv.IdSinhVien == id
                join cch in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
                select new ChuongTrinhHocDto
                {
                    IdChuongTrinhHoc = cch.IdChuongTrinhHoc,
                    TenChuongTrinhHoc = cch.TenChuongTrinhHoc
                }
            ).ToListAsync();

            return new ApiResponse<List<ChuongTrinhHocDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Lấy danh sách chương trình học thành công.",
                Data = query
            };
        }

        /**
         * Cập nhật thông tin chương trình học
         */
        public async Task<ApiResponse<bool>> Update(ChuongTrinhHocDto cth)
        {
            var cth_update = await _context.ChuongTrinhHocs
                .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == cth.IdChuongTrinhHoc);

            if (cth_update == null)
            {
                return new ApiResponse<bool>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Không tìm thấy chương trình học.",
                    Data = false
                };
            }

            cth_update.TenChuongTrinhHoc = cth.TenChuongTrinhHoc;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Status = true,
                StatusCode = 200,
                Message = "Cập nhật chương trình học thành công.",
                Data = true
            };
        }

        /**
         * Thêm chương trình học mới
         */
        public async Task<ApiResponse<bool>> Add(ChuongTrinhHocDto cth)
        {
            var cth_add = new ChuongTrinhHoc
            {
                IdChuongTrinhHoc = cth.IdChuongTrinhHoc,
                TenChuongTrinhHoc = cth.TenChuongTrinhHoc
            };

            await _context.ChuongTrinhHocs.AddAsync(cth_add);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Status = true,
                StatusCode = 201,
                Message = "Thêm chương trình học thành công.",
                Data = true
            };
        }

        /**
         * Xóa chương trình học theo ID
         */
        public async Task<ApiResponse<bool>> Delete(string id)
        {
            var cth_delete = await _context.ChuongTrinhHocs
                .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == id);

            if (cth_delete == null)
            {
                return new ApiResponse<bool>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Không tìm thấy chương trình học.",
                    Data = false
                };
            }

            _context.ChuongTrinhHocs.Remove(cth_delete);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Status = true,
                StatusCode = 200,
                Message = "Xóa chương trình học thành công.",
                Data = true
            };
        }

        /**
         * Lấy danh sách môn học có và không có trong chương trình học theo ID chương trình học
         */
        public async Task<ApiResponse<(List<MonHocDto> MonHocInChuongTrinhHoc, List<MonHocDto> MonHocKhongCo)>> GetMonHocByIdChuongTrinhHoc(string id)
        {
            var monHocInChuongTrinhHoc = await (
                from cch_mh in _context.ChuongTrinhHocMonHocs
                where cch_mh.IdChuongTrinhHoc == id
                join mh in _context.MonHocs on cch_mh.IdMonHoc equals mh.IdMonHoc
                select new MonHocDto
                {
                    IdMonHoc = mh.IdMonHoc,
                    TenMonHoc = mh.TenMonHoc,
                    SoTinChi = mh.SoTinChi,
                    SoTietHoc = mh.SoTietHoc,
                    IdKhoa = mh.IdKhoa
                }
            ).ToListAsync();

            var monHocKhongCo = await (
                from mh in _context.MonHocs
                where !(
                    from cch_mh in _context.ChuongTrinhHocMonHocs
                    where cch_mh.IdChuongTrinhHoc == id
                    select cch_mh.IdMonHoc
                ).Contains(mh.IdMonHoc)
                select new MonHocDto
                {
                    IdMonHoc = mh.IdMonHoc,
                    TenMonHoc = mh.TenMonHoc,
                    SoTinChi = mh.SoTinChi,
                    SoTietHoc = mh.SoTietHoc,
                    IdKhoa = mh.IdKhoa
                }
            ).ToListAsync();

            return new ApiResponse<(List<MonHocDto>, List<MonHocDto>)>
            {
                Status = true,
                StatusCode = 200,
                Message = "Lấy danh sách môn học thành công.",
                Data = (monHocInChuongTrinhHoc, monHocKhongCo)
            };
        }

        /**
         * Xóa môn học khỏi chương trình học
         */
        public async Task<ApiResponse<bool>> DeleteMonHocFromChuongTrinhHoc(string idChuongTrinhHoc, string idMonHoc)
        {
            var cth_mh_delete = await _context.ChuongTrinhHocMonHocs
                .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == idChuongTrinhHoc && x.IdMonHoc == idMonHoc);

            if (cth_mh_delete == null)
            {
                return new ApiResponse<bool>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Không tìm thấy môn học trong chương trình học.",
                    Data = false
                };
            }

            _context.ChuongTrinhHocMonHocs.Remove(cth_mh_delete);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Status = true,
                StatusCode = 200,
                Message = "Xóa môn học khỏi chương trình học thành công.",
                Data = true
            };
        }

        /**
         * Thêm môn học vào chương trình học
         */
        public async Task<ApiResponse<bool>> AddMonHocToChuongTrinhHoc(string idChuongTrinhHoc, string idMonHoc)
        {
            var cth_mh_add = new ChuongTrinhHocMonHoc
            {
                IdChuongTrinhHoc = idChuongTrinhHoc,
                IdMonHoc = idMonHoc
            };

            await _context.ChuongTrinhHocMonHocs.AddAsync(cth_mh_add);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Status = true,
                StatusCode = 201,
                Message = "Thêm môn học vào chương trình học thành công.",
                Data = true
            };
        }
    }
}