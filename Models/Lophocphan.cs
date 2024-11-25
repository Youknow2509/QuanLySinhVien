using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class LopHocPhan
{
    public string IdLopHocPhan { get; set; } = null!;

    public string TenHocPhan { get; set; } = null!;

    public string IdGiaoVien { get; set; } = null!;

    public string IdMonHoc { get; set; } = null!;

    public decimal SoTinChi { get; set; }

    public decimal SoTietHoc { get; set; }

    public DateTime ThoiGianBatDau { get; set; }

    public DateTime ThoiGianKetThuc { get; set; }

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual GiaoVien IdGiaoVienNavigation { get; set; } = null!;

    public virtual MonHoc IdMonHocNavigation { get; set; } = null!;

    public virtual ICollection<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; } = new List<SinhVienLopHocPhan>();

    public virtual ICollection<ThoiGianLopHocPhan> ThoiGianLopHocPhans { get; set; } = new List<ThoiGianLopHocPhan>();
}
