﻿using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class MonHoc
    {
        // Variables
        public string IdMonHoc { get; set; } = null!;
        public string? TenMonHoc { get; set; }
        public int? SoTinChi { get; set; }
        public int? SoTietHoc { get; set; }
        public string? IdKhoa { get; set; }

        // Variables left to another table
        public virtual Khoa? Khoas { get; set; }
        public virtual ICollection<ChuongTrinhHocMonHoc> ChuongTrinhHocMonHocs { get; set; }

        // Constructor
        public MonHoc()
        {
            ChuongTrinhHocMonHocs = new HashSet<ChuongTrinhHocMonHoc>();
        }
    }
}
