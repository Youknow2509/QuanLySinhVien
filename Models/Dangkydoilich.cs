using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Dangkydoilich
{
    public string Iddangkydoilich { get; set; } = null!;

    public string Idthoigian { get; set; } = null!;

    public DateTime Thoigianbatdauhientai { get; set; }

    public DateTime Thoigianketthuchientai { get; set; }

    public DateTime Thoigianbatdaumoi { get; set; }

    public DateTime Thoigianketthucmoi { get; set; }

    public decimal Trangthai { get; set; }

    public virtual Thoigian IdthoigianNavigation { get; set; } = null!;
}
