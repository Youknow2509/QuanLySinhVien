using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

//
using QLDT_WPF.Data;
using QLDT_WPF.Models;
using QLDT_WPF.Dto;

namespace QLDT_WPF.Repositories;

public class ChuongTrinhHocRepository : IDisposable
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public ChuongTrinhHocRepository()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["QuanLySinhVienDbContext"].ConnectionString;
        _context = new QuanLySinhVienDbContext(
            new DbContextOptionsBuilder<QuanLySinhVienDbContext>()
                .UseSqlServer(connectionString)
                .Options);
    }

    public void Dispose()
    {
        // Gọi Dispose() của _context để giải phóng kết nối cơ sở dữ liệu
        _context.Dispose();
    }

    /**
     * Lay tat ca chuong trinh hoc
     */
    public async Task<List<ChuongTrinhHocDto>> GetAll()
    {
        var list_cth = await _context.ChuongTrinhHocs
            .Select(x => new ChuongTrinhHocDto
            {
                IdChuongTrinhHoc = x.IdChuongTrinhHoc,
                TenChuongTrinhHoc = x.TenChuongTrinhHoc
            })
            .ToListAsync();

        return list_cth;
    }

    /**
     * Lay chuong trinh hoc by id
     */
    public async Task<ChuongTrinhHocDto> GetById(string id)
    {
        var list_cth = await _context.ChuongTrinhHocs
            .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == id);

        return new ChuongTrinhHocDto
        {
            IdChuongTrinhHoc = list_cth.IdChuongTrinhHoc,
            TenChuongTrinhHoc = list_cth.TenChuongTrinhHoc
        };
    }

    /**
     * Lay chuong trinh hoc by id sinh vien
     */
    public async Task<List<ChuongTrinhHocDto>> GetByIdSinhVien(string id)
    {
        // Query
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

        return query;
    }

    /**
     * Sua thong tin chuong trinh hoc
     */
    public async Task<bool> Update(ChuongTrinhHocDto cth)
    {
        var cth_update = await _context.ChuongTrinhHocs
            .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == cth.IdChuongTrinhHoc);

        if (cth_update == null)
        {
            return false;
        }

        cth_update.TenChuongTrinhHoc = cth.TenChuongTrinhHoc;

        await _context.SaveChangesAsync();

        return true;
    }

    /**
     * Them chuong trinh hoc
     */
    public async Task<bool> Add(ChuongTrinhHocDto cth)
    {
        var cth_add = new ChuongTrinhHoc
        {
            IdChuongTrinhHoc = cth.IdChuongTrinhHoc,
            TenChuongTrinhHoc = cth.TenChuongTrinhHoc
        };

        await _context.ChuongTrinhHocs.AddAsync(cth_add);
        await _context.SaveChangesAsync();

        return true;
    }

    /**
     * Xoa chuong trinh hoc By Id 
     */
    public async Task<bool> Delete(string id)
    {
        var cth_delete = await _context.ChuongTrinhHocs
            .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == id);

        if (cth_delete == null)
        {
            return false;
        }

        _context.ChuongTrinhHocs.Remove(cth_delete);
        await _context.SaveChangesAsync();

        return true;
    }

    /**
     * Lay ra danh sach mon hoc co va khong co trong chuong trinh hoc by id chuong trinh hoc
     */
    public async Task<(List<MonHoc> MonHocInChuongTrinhHoc, List<MonHoc> MonHocKhongCo)> GetMonHocByIdChuongTrinhHoc(string id)
    {
        // Tim cac mon hoc dang co trong chuong trinh hoc
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
                IdKhoa = mh.IdKhoa,
            }
        ).ToListAsync();

        // Tim cac mon hoc khong co trong chuong trinh hoc
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
                IdKhoa = mh.IdKhoa,
            }
        ).ToListAsync();

        return (monHocInChuongTrinhHoc, monHocKhongCo);
    }

    /**
     * Xoa mon hoc khoi chuong trinh hoc
     */
    public async Task<bool> DeleteMonHocFromChuongTrinhHoc(string idChuongTrinhHoc, string idMonHoc)
    {
        var cth_mh_delete = await _context.ChuongTrinhHocMonHocs
            .FirstOrDefaultAsync(x => x.IdChuongTrinhHoc == idChuongTrinhHoc && x.IdMonHoc == idMonHoc);

        if (cth_mh_delete == null)
        {
            return false;
        }

        _context.ChuongTrinhHocMonHocs.Remove(cth_mh_delete);
        await _context.SaveChangesAsync();

        return true;
    }

    /**
     * Them mon hoc vao chuong trinh hoc
     */
    public async Task<bool> AddMonHocToChuongTrinhHoc(string idChuongTrinhHoc, string idMonHoc)
    {
        var cth_mh_add = new ChuongTrinhHocMonHoc
        {
            IdChuongTrinhHoc = idChuongTrinhHoc,
            IdMonHoc = idMonHoc
        };

        await _context.ChuongTrinhHocMonHocs.AddAsync(cth_mh_add);
        await _context.SaveChangesAsync();

        return true;
    }
}