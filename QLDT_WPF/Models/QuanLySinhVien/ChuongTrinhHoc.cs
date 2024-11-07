using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class ChuongTrinhHoc
    {
        // Variables
        public string IdChuongTrinhHoc { get; set; } = null!;
        public string? TenChuongTrinhHoc { get; set; }

        // Variables linked to another table
        public virtual ICollection<ChuongTrinhHocMonHoc> ChuongTrinhHocMonHocs { get; set; }
        public virtual ICollection<SinhVien> SinhViens { get; set; }

        // Constructor
        public ChuongTrinhHoc()
        {
            ChuongTrinhHocMonHocs = new HashSet<ChuongTrinhHocMonHoc>();
            SinhViens = new HashSet<SinhVien>();
        }


    }
}
