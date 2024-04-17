using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class UserGetter : IUserGetter
{
    private readonly HttpContext _httpContext;

    public UserGetter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }

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