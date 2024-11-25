using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class ThoiGian
{
    public string IdThoiGian { get; set; } = null!;

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }

    public string IdPhongHoc { get; set; } = null!;

    public virtual ICollection<DangKyDoiLich> DangKyDoiLichs { get; set; } = new List<DangKyDoiLich>();

    public virtual PhongHoc IdPhongHocNavigation { get; set; } = null!;

    public virtual ICollection<ThoiGianLopHocPhan> ThoiGianLopHocPhans { get; set; } = new List<ThoiGianLopHocPhan>();
}
