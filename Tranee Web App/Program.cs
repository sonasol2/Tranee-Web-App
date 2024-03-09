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
        // app.MapControllerRoute(
        //     name: "default",
        //     pattern: "{controller=Home}/{action=Index}/{id?}");
        
        // app.Run(async context =>
        //     {
        //         var toDoService = app.Services.GetService<IToDoList>();
        //         toDoService.SetTask(new Task(1, "first task"));
        //         await context.Response.WriteAsync($"Task: {toDoService?.GetTask()}");
        //     }
        //
        // );
        
        app.Run();
    }
}