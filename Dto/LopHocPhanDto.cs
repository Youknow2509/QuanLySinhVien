namespace qlsv.Dto
{
    public class LopHocPhanDto
    {
        public string? IdLopHocPhan { get; set; }
        public string TenLopHocPhan { get; set; } 
        public string IdGiaoVien { set; get; }
        public string IdMonHoc { set; get; }
        public DateTime ThoiGianBatDau { set; get; }
        public DateTime ThoiGianKetThuc { set; get; }
    }
}
