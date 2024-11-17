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
         * Thêm danh sách chương trình học từ file CSV
         */
        public async Task<ApiResponse<List<ChuongTrinhHocDto>>> 
            AddListChuongTrinhHocFromCSV(List<ChuongTrinhHocDto> listChuongTrinhHoc)
        {
            // Kiểm tra nếu danh sách null hoặc trống
            if (listChuongTrinhHoc == null || !listChuongTrinhHoc.Any())
            {
                return new ApiResponse<List<ChuongTrinhHocDto>>
                {
                    Status = false,
                    Message = "File Không Được Để Trống!",
                    Data = null,
                };
            }

            List<ChuongTrinhHocDto> listChuongTrinhHocError = new List<ChuongTrinhHocDto>();
            HashSet<string> processedIds = new HashSet<string>();

            // Kiểm tra các bản ghi trùng lặp trong danh sách CSV
            foreach (var monh in listChuongTrinhHoc)
            {
                if (processedIds.Contains(monh.IdMonHoc))
                {
                    monh.TenMonHoc = $"Chương Trình Học: {monh.TenMonHoc} lỗi trùng ID {monh.IdMonHoc} trong file CSV";
                    listChuongTrinhHocError.Add(monh);
                    continue;
                }

                processedIds.Add(monh.IdMonHoc);
            }

            // Loại bỏ các bản ghi trùng lặp khỏi danh sách trước khi kiểm tra với CSDL
            var uniquelistChuongTrinhHoc = listChuongTrinhHoc.Except(listChuongTrinhHocError).ToList();

            // Kiểm tra với cơ sở dữ liệu và thêm vào danh sách lỗi nếu cần
            foreach (var cch in uniquelistChuongTrinhHoc)
            {
                var cct_c = await _context.ChuongTrinhHocs
                    .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == cch.IdChuongTrinhHoc);
                if (cct_c != null)
                {
                    cch.TenChuongTrinhHoc = $"Chương Trình Học: {cch.TenChuongTrinhHoc} đã tồn tại trong CSDL";
                    listChuongTrinhHocError.Add(cch);
                    continue;
                }
                // Nếu không có lỗi, thêm vào CSDL
                await _context.ChuongTrinhHocs.AddAsync(new ChuongTrinhHocDto
                {
                    IdChuongTrinhHoc = cch.IdChuongTrinhHoc,
                    TenChuongTrinhHoc = cch.TenChuongTrinhHoc
                });
            }

            // Nếu có bất kỳ lỗi nào trong quá trình xử lý
            if (listChuongTrinhHocError.Any())
            {
                return new ApiResponse<List<ChuongTrinhHocDto>>
                {
                    Status = false,
                    Message = "Thêm Danh Sách Chương Trình Học Thất Bại! Có lỗi trong danh sách chương trình học.",
                    Data = listMonHocError,
                };
            }

            // Lưu thay đổi nếu mọi thứ thành công
            await _context.SaveChangesAsync();

            return new ApiResponse<List<ChuongTrinhHocDto>>
            {
                Status = true,
                Message = "Thêm Danh Sách Chương Trình Học Thành Công!",
                Data = listChuongTrinhHoc,
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
        public async Task<ApiResponse<(List<ChuongTrinhHocDto> MonHocInChuongTrinhHoc, List<ChuongTrinhHocDto> MonHocKhongCo)>> GetMonHocByIdChuongTrinhHoc(string id)
        {
            var monHocInChuongTrinhHoc = await (
                from cch_mh in _context.ChuongTrinhHocMonHocs
                where cch_mh.IdChuongTrinhHoc == id
                join mh in _context.MonHocs on cch_mh.IdMonHoc equals mh.IdMonHoc
                select new ChuongTrinhHocDto
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
                select new ChuongTrinhHocDto
                {
                    IdMonHoc = mh.IdMonHoc,
                    TenMonHoc = mh.TenMonHoc,
                    SoTinChi = mh.SoTinChi,
                    SoTietHoc = mh.SoTietHoc,
                    IdKhoa = mh.IdKhoa
                }
            ).ToListAsync();

            return new ApiResponse<(List<ChuongTrinhHocDto>, List<ChuongTrinhHocDto>)>
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