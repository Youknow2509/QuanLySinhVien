
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace QLDT_WPF.Dto
{
    public class SinhVienDto
    {

        [Required]
        public string? IdSinhVien { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string? IdKhoa { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string? IdChuongTrinhHoc { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [RegularExpression(@"^[a-zA-ZàáảãạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵđÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴĐ\s]*$"
            , ErrorMessage = "Tên không hợp lệ")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string? Lop { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string? DiaChi { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string? TenKhoa { get; set; }

        public string? SoDienThoai { get; set; }

        public string? Email { get; set; }
    }
}
