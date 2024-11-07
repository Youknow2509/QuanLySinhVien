

namespace QLDT_WPF.Dto
{
    public class NguyenVongThayDoiLichDto
    {
        // Variables
        public string? IdDangKyDoiLich { get; set; } = null!;
        public string IdThoiGian { get; set; } = null!;
        public DateTime ThoiGianBatDauHienTai { get; set; }
        public DateTime ThoiGianKetThucHienTai { get; set; }
        public DateTime ThoiGianBatDauMoi { get; set; }
        public DateTime ThoiGianKetThucMoi { get; set; }
    }
}
