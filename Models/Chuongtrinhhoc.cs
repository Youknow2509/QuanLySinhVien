using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Chuongtrinhhoc
{
    public string Idchuongtrinhhoc { get; set; } = null!;

    public string Tenchuongtrinhhoc { get; set; } = null!;

    public virtual ICollection<ChuongtrinhhocMonhoc> ChuongtrinhhocMonhocs { get; set; } = new List<ChuongtrinhhocMonhoc>();

    public virtual ICollection<Sinhvien> Sinhviens { get; set; } = new List<Sinhvien>();
}
