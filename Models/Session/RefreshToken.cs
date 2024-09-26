
namespace qlsv.Models;

public class RefreshToken
{
    public string Id { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime ExpiryDate { get; set; }
}
