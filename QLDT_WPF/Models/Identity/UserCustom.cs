using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace QLDT_WPF.Models;

public class UserCustom : IdentityUser
{

    public string? IdClaim { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ProfilePictureBase64 { get; set; }
    public byte[]? ProfilePicture { get; set; }

    // TODO: Add more properties
}