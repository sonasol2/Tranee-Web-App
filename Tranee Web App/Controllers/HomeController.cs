using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Tranee_Web_App;

[Route("/api/[controller]")]
[Authorize]
public class HomeController : Controller
{
    private ApplicationContext db;
    private ToDoList _toDoList;
    public HomeController(ApplicationContext context)
    {
        db = context;
        _toDoList = new ToDoList(context);
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var userName = HttpContext.User.FindFirst("userName")?.Value;
        // var t = db.ToDoTasks.Where(x => x.User.Name == userName).ToList();
        return Ok(_toDoList.AllTask(userName));
    }
    
    [HttpPut("add-task")]
    public IActionResult AddTasks([FromBody]ToDoTask toDoTask)
    {
        var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
        _toDoList.AddTask(toDoTask, userId);
        // db.ToDoTasks.Add(new ToDoTask(){TaskDescription = toDoTask.TaskDescription, UserId = userId});
        // db.SaveChanges();
        return Ok();
    }

    [HttpDelete("del-task")]
    public IActionResult DelTasks([FromBody]int id)
    {
            ToDoTask toDoTask = db.ToDoTasks.FirstOrDefault(p => p.Id == id);
            if (toDoTask != null)
            {
                db.ToDoTasks.Remove(toDoTask);
                db.SaveChanges();
                return Ok();
            } 
            return BadRequest();
    }
    
    [HttpPut("edit-task")]
    public IActionResult EditTasks([FromBody]int id)
    {
        return Ok();
    }

    [HttpPut("select-task")]
    public IActionResult SelectTasks([FromBody]int id)
    {
        ToDoTask toDoTask = db.ToDoTasks.FirstOrDefault(p => p.Id == id);
        if (toDoTask != null)
        {
            toDoTask.Selected =! toDoTask.Selected;
            db.SaveChanges();
            return Ok();
        } 
        return BadRequest();
    }
    
}