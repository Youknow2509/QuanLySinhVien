using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class ChuongTrinhHocMonHoc
    {
        // Variables
        public string IdCthmonHoc { get; set; } = null!;
        public string? IdChuongTrinhHoc { get; set; }
        public string? IdMonHoc { get; set; }

        // Variables linked to another table
        public virtual ChuongTrinhHoc? ChuongTrinhHocs { get; set; }
        public virtual MonHoc? MonHocs { get; set; }
    }
}
