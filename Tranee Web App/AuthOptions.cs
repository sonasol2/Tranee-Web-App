using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Tranee_Web_App;

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    private const string KEY = "mYsUperPuperSecretingsPassWord!192";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}