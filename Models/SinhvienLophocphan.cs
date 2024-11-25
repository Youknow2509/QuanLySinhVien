using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class SinhVienLopHocPhan
{
    public string IdSinhVienLopHocPhan { get; set; } = null!;

    public string IdSinhVien { get; set; } = null!;

    public string IdLopHocPhan { get; set; } = null!;

    public virtual LopHocPhan IdLopHocPhanNavigation { get; set; } = null!;

    public virtual SinhVien IdSinhVienNavigation { get; set; } = null!;
}
