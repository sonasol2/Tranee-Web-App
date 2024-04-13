using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class UserGetter : IUserGetter
{
    private readonly HttpContext _httpContext;
    
    public int GetUserById()
    {
        var nameId = int.Parse(_httpContext.User.FindFirst("userId")?.Value);
        return nameId;
    }

    public string? GetUserByName()
    {
        var name = _httpContext.User.FindFirst("userName")?.Value;
        return name;
    }
}