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
// [Authorize]
public class HomeController : Controller
{
    ApplicationContext db;
    public HomeController(ApplicationContext context)
    {
        db = context;
    }
    
    [HttpGet]
    public IActionResult Index([FromHeader]string userName)
    {
        var t = db.ToDoTasks.Where(x => x.User.Name == userName).ToList();
        return Ok(t);
    }
    
    [HttpPost("add-task")]
    public IActionResult AddTasks([FromBody]DataTransferModel value)
    {
        // var user = db.Users.FirstOrDefault(u => u.Name == value.userName);
        // var toDoTask = db.ToDoTasks.FirstOrDefault(t => t.User.Id == value.userId);
        db.ToDoTasks.Add(new ToDoTask(){TaskDescription = value.taskDescription, UserId = value.userId});
        
        // db.ToDoTasks.Add(new ToDoTask(){TaskDescription = value.taskDescription, UserId = value.userId});
        db.SaveChanges();
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
        var a = db.ToDoTasks.Find(id);
        a.Selected =! a.Selected;
        db.SaveChanges();
        return Ok();
    }
    
}