using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class PhongHoc
{
    public string IdPhongHoc { get; set; } = null!;

    public string TenPhongHoc { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public virtual ICollection<ThoiGian> ThoiGians { get; set; } = new List<ThoiGian>();
}
