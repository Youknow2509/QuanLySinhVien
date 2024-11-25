using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class MonHoc
{
    public string IdMonHoc { get; set; } = null!;

    public string TenMonHoc { get; set; } = null!;

    public string IdKhoa { get; set; } = null!;

    public virtual ICollection<ChuongTrinhHocMonHoc> ChuongTrinhHocMonHocs { get; set; } = new List<ChuongTrinhHocMonHoc>();

    public virtual ICollection<DangKyNguyenVong> DangKyNguyenVongs { get; set; } = new List<DangKyNguyenVong>();

    public virtual Khoa IdKhoaNavigation { get; set; } = null!;

    public virtual ICollection<LopHocPhan> LopHocPhans { get; set; } = new List<LopHocPhan>();
}
