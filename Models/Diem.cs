using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Diem
{
    public string Iddiem { get; set; } = null!;

    public string Idlophocphan { get; set; } = null!;

    public string Idsinhvien { get; set; } = null!;

    public decimal Diemquatrinh { get; set; }

    public decimal Diemketthuc { get; set; }

    public decimal Diemtongket { get; set; }

    public decimal Lanhoc { get; set; }

    public virtual Lophocphan IdlophocphanNavigation { get; set; } = null!;

    public virtual Sinhvien IdsinhvienNavigation { get; set; } = null!;
}
