using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Lophocphan
{
    public string Idlophocphan { get; set; } = null!;

    public string Tenhocphan { get; set; } = null!;

    public string Idgiaovien { get; set; } = null!;

    public string Idmonhoc { get; set; } = null!;

    public decimal Sotinchi { get; set; }

    public decimal Sotiethoc { get; set; }

    public DateTime Thoigianbatdau { get; set; }

    public DateTime Thoigianketthuc { get; set; }

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual Giaovien IdgiaovienNavigation { get; set; } = null!;

    public virtual Monhoc IdmonhocNavigation { get; set; } = null!;

    public virtual ICollection<SinhvienLophocphan> SinhvienLophocphans { get; set; } = new List<SinhvienLophocphan>();

    public virtual ICollection<ThoigianLophocphan> ThoigianLophocphans { get; set; } = new List<ThoigianLophocphan>();
}
