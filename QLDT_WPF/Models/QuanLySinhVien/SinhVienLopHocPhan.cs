using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class SinhVienLopHocPhan
    {
        // Variables
        public string IdSinhVienLopHp { get; set; } = null!;
        public string? IdSinhVien { get; set; }
        public string? IdLopHocPhan { get; set; }

        // Variables linked to another table
        public virtual LopHocPhan? LopHocPhans { get; set; }
        public virtual SinhVien? SinhViens { get; set; }
    }
}
