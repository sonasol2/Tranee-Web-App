using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Tranee_Web_App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlite(connection));
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
        builder.Services.AddTransient<IToDoList, ToDoList>();
        // builder.Services.AddTransient<IToDoListy, ToDoListy>();
        // builder.Services.AddTransient<IToDoAdder, ToDoAdder>();
        // builder.Services.AddTransient<IUserGetter, UserGetter>();
        // builder.Services.AddTransient<IToDoTaskReader, ToDoTaskReader>();
        // builder.Services.AddTransient<IToDoBinder, ToDoBinder>();
        
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