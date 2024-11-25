using System;
using System.Collections.Generic;

namespace web_qlsv.Models;

public class DangKyDoiLich
{
    public string IdDangKyDoiLich { get; set; } = null!;

    public string IdThoiGian { get; set; } = null!;

    public DateTime ThoiGianBatDauHienTai { get; set; }

    public DateTime ThoiGianKetThucHienTai { get; set; }

    public DateTime ThoiGianBatDauMoi { get; set; }

    public DateTime ThoiGianKetThucMoi { get; set; }

    public decimal TrangThai { get; set; }

    public virtual ThoiGian IdThoiGianNavigation { get; set; } = null!;
}
