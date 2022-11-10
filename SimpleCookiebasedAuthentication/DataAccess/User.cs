namespace SimpleCookiebasedAuthentication.DataAccess;
public class User
{
    public enum UserRole { User, Administrator }
    public int Id { get; set; }
    public string UserName { get; set; } = "unnamed";
    public string? Email { get; set; }
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}