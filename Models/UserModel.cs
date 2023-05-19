namespace DutyFree.Models;

public class UserModel
{
    public int UserId { get; set; }
    public string Name{ get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public UserRole Role { get; set; }
}

public enum UserRole
{
    User = 0,
    Admin = 1
}