
namespace QLDT_WPF.Models;

public class AccessToken
{
    public string Id { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
    public DateTime ExpiryDate { get; set; }
}
