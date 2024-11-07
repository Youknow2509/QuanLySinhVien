using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class LopHocPhan
    {
        // Variables
        public string IdLopHocPhan { get; set; } = null!;
        public string TenHocPhan { get; set; } = null!;
        public string? IdGiaoVien { get; set; }
        public string? IdMonHoc { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }

        // Variables linked to another table
        public virtual GiaoVien? GiaoViens { get; set; }
        public virtual MonHoc? MonHocs { get; set; }
        public virtual ICollection<Diem> Diems { get; set; }
        public virtual ICollection<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; }
        public virtual ICollection<ThoiGianLopHocPhan> ThoiGianLopHocPhans { get; set; }

        // Constructor
        public LopHocPhan()
        {
            Diems = new HashSet<Diem>();
            SinhVienLopHocPhans = new HashSet<SinhVienLopHocPhan>();
            ThoiGianLopHocPhans = new HashSet<ThoiGianLopHocPhan>();
        }
    }
}
