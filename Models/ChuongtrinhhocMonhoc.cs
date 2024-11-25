using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class ChuongtrinhhocMonhoc
{
    public string Idcthmonhoc { get; set; } = null!;

    public string Idchuongtrinhhoc { get; set; } = null!;

    public string Idmonhoc { get; set; } = null!;

    public virtual Chuongtrinhhoc IdchuongtrinhhocNavigation { get; set; } = null!;

    public virtual Monhoc IdmonhocNavigation { get; set; } = null!;
}
