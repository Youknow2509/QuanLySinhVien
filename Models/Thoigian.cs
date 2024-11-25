using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Thoigian
{
    public string Idthoigian { get; set; } = null!;

    public DateTime Ngaybatdau { get; set; }

    public DateTime Ngayketthuc { get; set; }

    public string Idphonghoc { get; set; } = null!;

    public virtual ICollection<Dangkydoilich> Dangkydoiliches { get; set; } = new List<Dangkydoilich>();

    public virtual Phonghoc IdphonghocNavigation { get; set; } = null!;

    public virtual ICollection<ThoigianLophocphan> ThoigianLophocphans { get; set; } = new List<ThoigianLophocphan>();
}
