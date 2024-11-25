using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public  class ThoiGianLopHocPhan
{
    public string IdThoiGianLopHocPhan { get; set; } = null!;

    public string IdLopHocPhan { get; set; } = null!;

    public string IdThoiGian { get; set; } = null!;

    public virtual LopHocPhan IdLopHocPhanNavigation { get; set; } = null!;

    public virtual ThoiGian IdThoiGianNavigation { get; set; } = null!;
}
