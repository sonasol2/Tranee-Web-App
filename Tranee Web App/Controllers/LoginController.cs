using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Tranee_Web_App;


[Route("/api/[controller]")]
public class LoginController : Controller
{
    ApplicationContext db;
    AuthOptions _options;

    public LoginController(ApplicationContext context)
    {
        db = context;
    }

    [HttpGet]
    // [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("login")]
    // [Authorize]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody]User loginData)
    {
        User? user = db.Users.FirstOrDefault(p => p.Name == loginData.Name && p.Password == loginData.Password);
        if (user is null) return Unauthorized();
        var claims = new List<Claim> { new Claim("userName", user.Name), new Claim("userId", user.Id.ToString()) };
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(100)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        // var u = ClaimsPrincipal.Current.Identity;
        var response = new
        {
            access_token = encodedJwt,
            username = user.Name,
            userid = user.Id,
            // u = u
        };
        
        return Ok(response);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
    
    [HttpGet("AddUser")]
    [Authorize]
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