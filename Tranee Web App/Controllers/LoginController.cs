using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        // var key = Request.Headers.Authorization;
    }

    [HttpGet]
    // [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Login")]
    // [Authorize]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost("Login")]
    public IActionResult Login([FromBody]User loginData)
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

        // Response.Cookies.Append("X-Access-Token", response.access_token, new CookieOptions() {HttpOnly = true, SameSite = SameSiteMode.Strict});
        // Response.Cookies.Append("X-Username", response.username, new CookieOptions() {HttpOnly = true, SameSite = SameSiteMode.Strict});

        return Json(response);
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