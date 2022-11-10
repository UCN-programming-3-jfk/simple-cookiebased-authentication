using static SimpleCookiebasedAuthentication.DataAccess.User;

namespace SimpleCookiebasedAuthentication.DataAccess;
public class UserProvider : IUserProvider
{
    public User? GetUserByLogin(string userName, string password)
    {
        //This code simulates accessing a database
        //and retrieving the user based on the username and password

        if (userName == "Bob" && password == "1234")
        {
            return new User() { Id = 42, UserName = "Bob", Email = "bob@gmail.com", Password = "1234", Role = UserRole.User };
        }
        else if (userName == "Admin" && password == "1234")
        {
            return new User() { Id = 1337, UserName = "Administrator", Email = "admin@gmail.com", Password = "1234", Role = UserRole.Administrator };
        }
        else return null;
    }
}