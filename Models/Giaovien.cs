using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Giaovien
{
    public string Idgiaovien { get; set; } = null!;

    public string Tengiaovien { get; set; } = null!;

    public string? Email { get; set; }

    public string? Sodienthoai { get; set; }

    public string Idkhoa { get; set; } = null!;

    public virtual Khoa IdkhoaNavigation { get; set; } = null!;

    public virtual ICollection<Lophocphan> Lophocphans { get; set; } = new List<Lophocphan>();
}
