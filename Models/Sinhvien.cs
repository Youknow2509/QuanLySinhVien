using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class SinhVien
{
    public string IdSinhVien { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string Lop { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string? DiaChi { get; set; }

    public string IdChuongTrinhHoc { get; set; } = null!;

    public string IdKhoa { get; set; } = null!;

    public virtual ICollection<DangKyNguyenVong> DangKyNguyenVongs { get; set; } = new List<DangKyNguyenVong>();

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual ChuongTrinhHoc IdChuongTrinhHocNavigation { get; set; } = null!;

    public virtual Khoa IdKhoaNavigation { get; set; } = null!;

    public virtual ICollection<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; } = new List<SinhVienLopHocPhan>();
}
