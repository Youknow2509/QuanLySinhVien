using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class ThoiGianLopHocPhan
    {
        // Variables
        public string IdThoigianLopHp { get; set; } = null!;
        public string? IdLopHocPhan { get; set; }
        public string? IdThoiGian { get; set; }

        // Variables linked to another table
        public virtual LopHocPhan? LopHocPhans { get; set; }
        public virtual ThoiGian? ThoiGians { get; set; }
    }
}
