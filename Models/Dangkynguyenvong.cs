using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Dangkynguyenvong
{
    public string Iddangkynguyenvong { get; set; } = null!;

    public string Idsinhvien { get; set; } = null!;

    public string Idmonhoc { get; set; } = null!;

    public decimal Trangthai { get; set; }

    public virtual Monhoc IdmonhocNavigation { get; set; } = null!;

    public virtual Sinhvien IdsinhvienNavigation { get; set; } = null!;
}
