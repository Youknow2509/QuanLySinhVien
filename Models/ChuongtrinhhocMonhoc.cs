using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class ChuongTrinhHocMonHoc
{
    public string IdChuongTrinhHocMonHoc { get; set; } = null!;

    public string IdChuongTrinhHoc { get; set; } = null!;

    public string IdMonHoc { get; set; } = null!;

    public virtual ChuongTrinhHoc IdChuongTrinhHocNavigation { get; set; } = null!;

    public virtual MonHoc IdMonHocNavigation { get; set; } = null!;
}
