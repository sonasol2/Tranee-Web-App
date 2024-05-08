using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface ILoginService
{
    Task<bool> ValidateCredentials(User user, string password);
    Task<User> FindByUsername(string user);
    Task SignIn(User user);
}