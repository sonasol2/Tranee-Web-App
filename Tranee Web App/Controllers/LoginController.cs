using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Tranee_Web_App;


// [Route("[controller]}")]
public class LoginController : Controller
{
    ApplicationContext db;
    AuthOptions _options;
    

    public LoginController(ApplicationContext context)
    {
        db = context;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return Ok();
    }
    
    [HttpPost("/")]
    public IActionResult Index(User loginData)
    {
        User? user = db.Users.FirstOrDefault(p => p.Name == loginData.Name && p.Password == loginData.Password);
        if (user is null) return Unauthorized();
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name) };
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    
        var response = new
        {
            access_token = encodedJwt,
            username = user.Name
        };
        return Json(response);
    }

    [HttpGet("AddUser")]
    public IActionResult AddUser()
    {
        return Ok();
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(User user)
    {
        db.Users.Add(user);
        db.SaveChanges();
        return RedirectToAction("AddUser");
    }
}