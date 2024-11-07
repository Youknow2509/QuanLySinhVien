using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class Diem
    {
        // Variables
        public string IdDiem { get; set; } = null!;
        public string? IdLopHocPhan { get; set; }
        public string? IdSinhVien { get; set; }
        public decimal? DiemQuaTrinh { get; set; }
        public decimal? DiemKetThuc { get; set; }
        public decimal? DiemTongKet { get; set; }
        public int? LanHoc { get; set; }

        // Variables linked to another table
        public virtual LopHocPhan? LopHocPhans { get; set; }
        public virtual SinhVien? SinhViens { get; set; }
    }
}
