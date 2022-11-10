namespace SimpleCookiebasedAuthentication.DataAccess;
public interface IUserProvider
{
    User? GetUserByLogin(string userName, string password);
}