using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class GiaoVien
{
    public string IdGiaoVien { get; set; } = null!;

    public string TenGiaoVien { get; set; } = null!;

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string IdKhoa { get; set; } = null!;

    public virtual Khoa? IdKhoaNavigation { get; set; } = null!;

    public virtual ICollection<LopHocPhan>? LopHocPhans { get; set; } = new List<LopHocPhan>();
}
