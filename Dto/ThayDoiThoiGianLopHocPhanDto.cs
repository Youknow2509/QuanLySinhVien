
namespace web_qlsv.Dto;

public class ThayDoiThoiGianLopHocPhanDto
{
    // Variables
    public string IdThoiGian { get; set; }
    public string IdLopHocPhan { get; set; }
    public DateTime ThoiGianBatDau { get; set; }
    public DateTime ThoiGianKetThuc { get; set; }
    public string? IdPhongHoc { get; set; }
}