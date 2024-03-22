using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

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
    
    public JsonResult Index()
    {
        if (db.ToDoTasks == null) return Json("Empty List");
        // return View(db.ToDoTasks.ToList());
        return Json(db.ToDoTasks);
    }

    // [HttpGet("AddTask")]    
    // public IActionResult AddTask()
    // {
    //     return View();
    // }
    
    [HttpPost("AddTask")]
    public JsonResult AddTasks([FromBody]ToDoTask toDoTask)
    {
        // db.Users.Add(user);
        db.ToDoTasks.Add(toDoTask);
        db.SaveChanges();
        // return RedirectToAction("Index");
        return Json(Ok());
    }

    [HttpPost("DelTask")]
    public IActionResult DelTasks(int id)
    {
        if (id != null)
        {
            ToDoTask toDoTask = db.ToDoTasks.FirstOrDefault(p => p.Id == id);
            if (toDoTask != null)
            {
                db.ToDoTasks.Remove(toDoTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        return Ok();
    }

    [HttpPost("SelectTask")]
    public IActionResult SelectTasks(int id)
    {
        var a = db.ToDoTasks.Find(id);
        a.Selected =! a.Selected;
        return RedirectToAction("Index");
    }
    
}