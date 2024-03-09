using Microsoft.AspNetCore.Mvc;

namespace Tranee_Web_App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllersWithViews();
        builder.Services.AddSingleton<IToDoList, ToDoList>();
        builder.Services.AddSwaggerGen();
        
        var app = builder.Build();
        
        app.UseSwaggerUI();
        app.UseSwagger();
        
        app.MapControllers();
     
        
        app.Run();
    }
}