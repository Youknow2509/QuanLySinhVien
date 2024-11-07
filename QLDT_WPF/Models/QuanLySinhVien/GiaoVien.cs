using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class GiaoVien
    {
        // Variables
        public string IdGiaoVien { get; set; } = null!;
        public string TenGiaoVien { get; set; } = null!;
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? IdKhoa { get; set; }

        // Variables linked to another table
        public virtual Khoa? Khoas { get; set; }
        public virtual ICollection<LopHocPhan> LopHocPhans { get; set; }

        // Constructor
        public GiaoVien()
        {
            LopHocPhans = new HashSet<LopHocPhan>();
        }
    }
}
