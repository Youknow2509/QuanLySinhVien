using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public class Khoa
{
    public string IdKhoa { get; set; } = null!;

    public string TenKhoa { get; set; } = null!;

    public virtual ICollection<GiaoVien> GiaoViens { get; set; } = new List<GiaoVien>();

    public virtual ICollection<MonHoc> MonHocs { get; set; } = new List<MonHoc>();

    public virtual ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
}
