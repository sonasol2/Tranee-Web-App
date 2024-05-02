using System.Security.Claims;
using Tranee_Web_App.Models;
using Duende.IdentityServer.Logging.Models;

namespace Tranee_Web_App.Services;

public class LoginService<User> : ILoginService<User>
{
    // private BaseUser _baseUser;
    
    
    public Task<bool> ValidateCredentials(User user, string password)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByUsername(string user)
    {
        throw new NotImplementedException();
    }

    public Task SignIn(User user)
    {
        throw new NotImplementedException();
    }
}