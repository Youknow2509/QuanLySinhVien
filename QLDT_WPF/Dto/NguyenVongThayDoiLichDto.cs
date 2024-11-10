

namespace QLDT_WPF.Dto
{
    public class NguyenVongThayDoiLichDto
    {
        // Variables
        public string? IdDangKyDoiLich { get; set; }
        public string? IdThoiGian { get; set; }
        public string? IdLopHocPhan { get; set; }

        public DateTime? ThoiGianBatDauHienTai { get; set; }
        public DateTime? ThoiGianKetThucHienTai { get; set; }
        public DateTime? ThoiGianBatDauMoi { get; set; }
        public DateTime? ThoiGianKetThucMoi { get; set; }
        public int? TrangThai { get; set; } 
        public string? TenLopHocPhan { get; set; }
    }
}
