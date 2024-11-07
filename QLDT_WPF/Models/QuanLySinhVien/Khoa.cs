using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class Khoa
    {
        // Variables
        public string IdKhoa { get; set; } = null!;
        public string? TenKhoa { get; set; }

        // Variables linked to another table
        public virtual ICollection<GiaoVien> GiaoViens { get; set; }
        public virtual ICollection<MonHoc> MonHocs { get; set; }
        public virtual ICollection<SinhVien> SinhViens { get; set; }

        // Constructor
        public Khoa()
        {
            GiaoViens = new HashSet<GiaoVien>();
            MonHocs = new HashSet<MonHoc>();
        }
    }
}
