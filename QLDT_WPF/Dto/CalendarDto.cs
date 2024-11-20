using System.ComponentModel.DataAnnotations;

namespace QLDT_WPF.Dto
{
    public class CalendarDto
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? GroupId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public Boolean? DisplayEventTime { get; set; }
        public string? Location { get; set; }
        public string? StatusMessage { get; set; }
    }
}