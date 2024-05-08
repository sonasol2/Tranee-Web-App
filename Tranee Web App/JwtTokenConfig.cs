using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Tranee_Web_App;

public class JwtTokenConfig
{
    // public void GetJwtToken()
    // {
    //     var jwt = new JwtSecurityToken(
    //         issuer: AuthOptions.ISSUER,
    //         audience: AuthOptions.AUDIENCE,
    //         claims: claims,
    //         expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(100)),
    //         signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
    //             SecurityAlgorithms.HmacSha256));
    //     var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    // }
}