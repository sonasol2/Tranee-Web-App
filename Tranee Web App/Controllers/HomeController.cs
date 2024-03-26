using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    public IActionResult Index()
    {
        // if (db.ToDoTasks == null) return Json("Empty List");
        return Ok(db);
    }
    
    [HttpPost("add-task")]
    public IActionResult AddTasks([FromBody]ToDoTask toDoTask)
    {
        db.ToDoTasks.Add(toDoTask);
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