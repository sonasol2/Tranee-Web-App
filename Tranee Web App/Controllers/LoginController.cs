using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Tranee_Web_App.Services;

namespace Tranee_Web_App;


[Route("/api/[controller]")]
public class LoginController : Controller
{
    ILoginService _loginService;
    ApplicationContext db;
    AuthOptions _options;

    public LoginController(ApplicationContext context, ILoginService loginService)
    {
        db = context;
        _loginService = loginService;
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
    
    // [HttpPost("login")]
    // public IActionResult Login([FromBody]User loginData, ClaimsPrincipal principal)
    // {
    //     if (loginData.Name == "" || loginData.Password == "")
    //         return BadRequest("Name or Pass is not defined");
    //     User? user = db.Users.FirstOrDefault(p => p.Name == loginData.Name && p.Password == loginData.Password);
    //     if (user is null) return Unauthorized();
    //     var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name), new Claim("userId", user.Id.ToString()) };
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
        // var admin = new User() { Id = 1, Name = "Admin", Password = "123", Role = new Role(){Name = "Admin"}};
        // db.Users.Add(admin);
        // db.SaveChanges();
        User? person = db.Users.FirstOrDefault(u => u.Name == data.Name && u.Password == data.Password);
        if (person is null) return Unauthorized();
        Role role = db.Roles.FirstOrDefault(r => r.Id == person.RoleId);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, person.Name), 
            new Claim("userId", person.Id.ToString()),
            new Claim( ClaimTypes.Role, role.Name)
        };
        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);
        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        return Ok();
    }
//     
    
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Данные удалены");
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
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name) };
        db.Users.Add(user);
        db.SaveChanges();
        return RedirectToAction("AddUser");
    }
}