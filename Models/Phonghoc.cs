using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public partial class Phonghoc
{
    public string Idphonghoc { get; set; } = null!;

    public string Tenphonghoc { get; set; } = null!;

    public string Diachi { get; set; } = null!;

    public virtual ICollection<Thoigian> Thoigians { get; set; } = new List<Thoigian>();
}
