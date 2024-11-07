
namespace QLDT_WPF.Models;

public class CalendarEvent
{
    public string Id { get; set; }  // Mã định danh duy nhất của sự kiện.
    public string Title { get; set; }  // Tiêu đề của sự kiện.
    
    public DateTime Start { get; set; }  // Thời gian bắt đầu sự kiện.
    public DateTime? End { get; set; }  // Thời gian kết thúc sự kiện (nullable).
    
    public bool AllDay { get; set; }  // Xác định sự kiện có phải là cả ngày hay không.

    public string BackgroundColor { get; set; }  // Màu nền của sự kiện.
    public string BorderColor { get; set; }  // Màu viền sự kiện.
    public string TextColor { get; set; }  // Màu chữ của sự kiện.

    public string Description { get; set; }  // Mô tả chi tiết sự kiện.
    public string Location { get; set; }  // Địa điểm của sự kiện.

    public bool Editable { get; set; }  // Có thể chỉnh sửa sự kiện hay không.
    public string Url { get; set; }  // Liên kết chi tiết sự kiện (nếu có).
    
    public string RecurrenceRule { get; set; }  // Quy tắc lặp lại (RRULE) của sự kiện.
}

