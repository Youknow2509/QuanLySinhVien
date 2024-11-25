using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class ThoigianLophocphan
{
    public string Idthoigianlophp { get; set; } = null!;

    public string Idlophocphan { get; set; } = null!;

    public string Idthoigian { get; set; } = null!;

    public virtual Lophocphan IdlophocphanNavigation { get; set; } = null!;

    public virtual Thoigian IdthoigianNavigation { get; set; } = null!;
}
