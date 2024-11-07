using System.ComponentModel.DataAnnotations;

namespace QLDT_WPF.Dto
{
    public class UpdateRootDto
    {
        [Required]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [RegularExpression(@"^[a-zA-ZàáảãạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵđÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴĐ\s]*$"
            , ErrorMessage = "Tên không hợp lệ")]
        public string? FullName { get; set; }


        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [DataType(DataType.MultilineText, ErrorMessage = "Địa chỉ không hợp lệ")]
        public string? Address { get; set; }

        public byte[]? ProfilePicture { get; set; }

    }
}