using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class DangKyNguyenVong
{
    public string IdDangKyNguyenVong { get; set; } = null!;

    public string IdSinhVien { get; set; } = null!;

    public string IdMonHoc { get; set; } = null!;

    public decimal TrangThai { get; set; }

    public virtual MonHoc IdMonHocNavigation { get; set; } = null!;

    public virtual SinhVien IdSinhVienNavigation { get; set; } = null!;
}
