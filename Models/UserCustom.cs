
using Microsoft.AspNetCore.Identity;

namespace qlsv.Models;

public class UserCustom: IdentityUser {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public byte[]? ProfilePicture { get; set; }

    // TODO: Add more properties
}