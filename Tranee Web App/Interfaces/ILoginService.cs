namespace Tranee_Web_App;

public interface ILoginService<T>
{
    Task<bool> ValidateCredentials(T user, string password);
    Task<T> FindByUsername(string user);
    Task SignIn(T user);
}