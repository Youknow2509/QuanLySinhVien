namespace QLDT_WPF.Dto
{
    public class DiemDto
    {
        public string? IdDiem { get; set; }
        public string IdLopHocPhan { get; set; }
        public string IdSinhVien { get; set; }
        public string? IdMon { get; set; }
        
        public decimal DiemQuaTrinh { get; set; }
        public decimal DiemKetThuc { get; set; }
        public decimal DiemTongKet { get; set; }
        public int LanHoc { get; set; }
        public string? TenMonHoc { get; set; }
        public string? TenLopHocPhan { get; set; }
    }
}
