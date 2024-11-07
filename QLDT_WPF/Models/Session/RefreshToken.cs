
namespace QLDT_WPF.Models;

public class RefreshToken
{
    public string Id { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime ExpiryDate { get; set; }
}
