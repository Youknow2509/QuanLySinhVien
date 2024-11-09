
using QLDT_WPF.Dto;

public class UserDto
{
    public string? Id { get; set; }
    public string? IdClaim { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Phone{ get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public string? ProfilePictureBase64 { get; set; }
    public string? PasswordHash { get; set; }
    public string? IdRole { get; set; }
    public string? RoleName { get; set; }
}