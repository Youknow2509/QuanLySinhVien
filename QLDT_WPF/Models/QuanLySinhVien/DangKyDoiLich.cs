using System;
using System.Collections.Generic;

namespace QLDT_WPF.Models
{
    public class DangKyDoiLich
    {
        // Variables
        public string? IdDangKyDoiLich { get; set; } = null!;
        public string IdThoiGian { get; set; } = null!;
        public DateTime ThoiGianBatDauHienTai { get; set; }
        public DateTime ThoiGianKetThucHienTai { get; set; }
        public DateTime ThoiGianBatDauMoi { get; set; }
        public DateTime ThoiGianKetThucMoi { get; set; }
        public int TrangThai { get; set; }

        public virtual ThoiGian? ThoiGians { get; set; }
    }
}
