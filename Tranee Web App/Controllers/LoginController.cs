using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

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

    // [HttpGet("login")]
    // // [Authorize]
    // public IActionResult Login()
    // {
    //     return View();
    // }
    
    // [HttpPost("login")]
    // public IActionResult Login([FromBody]User loginData)
    // {
    //     User? user = db.Users.FirstOrDefault(p => p.Name == loginData.Name && p.Password == loginData.Password);
    //     if (user is null) return Unauthorized();
    //     var claims = new List<Claim> { new Claim("userName", user.Name), new Claim("userId", user.Id.ToString()) };
    //     var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
    //     var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
    //     var jwt = new JwtSecurityToken(
    //         issuer: AuthOptions.ISSUER,
    //         audience: AuthOptions.AUDIENCE,
    //         claims: claims,
    //         expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(100)),
    //         signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
    //             SecurityAlgorithms.HmacSha256));
    //     var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    //     HttpContext.SignInAsync(claimsPrincipal);
    //     var response = new
    //     {
    //         access_token = encodedJwt,
    //         username = user.Name,
    //         userid = user.Id,
    //     };
    //     
    //     return Ok(response);
    // }
[HttpPost("login")]
    public IActionResult Login([FromBody]User data)
    {
        User? person = db.Users.FirstOrDefault(u => u.Name == data.Name && u.Password == data.Password);
        if (person is null) return Unauthorized();
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, data.Name) };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return Ok();
    }
    
    
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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