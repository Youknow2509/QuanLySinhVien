using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class SinhvienLophocphan
{
    public string Idsinhvienlophp { get; set; } = null!;

    public string Idsinhvien { get; set; } = null!;

    public string Idlophocphan { get; set; } = null!;

    public virtual Lophocphan IdlophocphanNavigation { get; set; } = null!;

    public virtual Sinhvien IdsinhvienNavigation { get; set; } = null!;
}
