using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Tranee_Web_App.Models;
using Tranee_Web_App.Services;

namespace Tranee_Web_App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlite(connection));
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/api/Login/login";
            });
        // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer(options =>
        //         {
        //             options.TokenValidationParameters = new TokenValidationParameters
        //             {
        //                 ValidateIssuer = true,
        //                 ValidIssuer = AuthOptions.ISSUER,
        //                 ValidateAudience = true,
        //                 ValidAudience = AuthOptions.AUDIENCE,
        //                 ValidateLifetime = true,
        //                 IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        //                 ValidateIssuerSigningKey = true,
        //             };
        //         });
        builder.Services.AddTransient<IToDoListService, ToDoListService>();
        builder.Services.AddTransient<IRepository<ToDoTask>, ToDoRepository>();
        builder.Services.AddTransient<ILoginService, LoginService>();
        
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        
        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseSwaggerUI();
        app.UseSwagger();
        
        app.MapControllers();
        
        app.Run();
    }
}