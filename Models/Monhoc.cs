using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Monhoc
{
    public string Idmonhoc { get; set; } = null!;

    public string Tenmonhoc { get; set; } = null!;

    public string Idkhoa { get; set; } = null!;

    public virtual ICollection<ChuongtrinhhocMonhoc> ChuongtrinhhocMonhocs { get; set; } = new List<ChuongtrinhhocMonhoc>();

    public virtual ICollection<Dangkynguyenvong> Dangkynguyenvongs { get; set; } = new List<Dangkynguyenvong>();

    public virtual Khoa IdkhoaNavigation { get; set; } = null!;

    public virtual ICollection<Lophocphan> Lophocphans { get; set; } = new List<Lophocphan>();
}
