using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class ChuongTrinhHoc
{
    public string IdChuongTrinhHoc { get; set; } = null!;

    public string TenChuongTrinhHoc { get; set; } = null!;

    public virtual ICollection<ChuongTrinhHocMonHoc> ChuongTrinhHocMonHocs { get; set; } = new List<ChuongTrinhHocMonHoc>();

    public virtual ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
}
