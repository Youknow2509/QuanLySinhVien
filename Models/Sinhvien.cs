using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Sinhvien
{
    public string Idsinhvien { get; set; } = null!;

    public string Hoten { get; set; } = null!;

    public string Lop { get; set; } = null!;

    public DateTime Ngaysinh { get; set; }

    public string? Diachi { get; set; }

    public string Idchuongtrinhhoc { get; set; } = null!;

    public string Idkhoa { get; set; } = null!;

    public virtual ICollection<Dangkynguyenvong> Dangkynguyenvongs { get; set; } = new List<Dangkynguyenvong>();

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual Chuongtrinhhoc IdchuongtrinhhocNavigation { get; set; } = null!;

    public virtual Khoa IdkhoaNavigation { get; set; } = null!;

    public virtual ICollection<SinhvienLophocphan> SinhvienLophocphans { get; set; } = new List<SinhvienLophocphan>();
}
