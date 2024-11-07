using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class DangKyNguyenVong
    {
        // Variables
        public string IdDangKyNguyenVong { get; set; } = null!;
        public string IdSinhVien { get; set; }
        public string IdMonHoc { get; set; }
        public int TrangThai { get; set; }

        // Variables linked to another table
        public virtual SinhVien? SinhViens { get; set; }
        public virtual MonHoc? MonHocs { get; set; }
    }
}
