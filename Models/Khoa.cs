using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Khoa
{
    public string Idkhoa { get; set; } = null!;

    public string Tenkhoa { get; set; } = null!;

    public virtual ICollection<Giaovien> Giaoviens { get; set; } = new List<Giaovien>();

    public virtual ICollection<Monhoc> Monhocs { get; set; } = new List<Monhoc>();

    public virtual ICollection<Sinhvien> Sinhviens { get; set; } = new List<Sinhvien>();
}
