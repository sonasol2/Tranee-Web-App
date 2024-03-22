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
    public JsonResult Index()
    {
        if (db.ToDoTasks == null) return Json("Empty List");
        return Json(db.ToDoTasks);
    }
    
    [HttpPost("AddTask")]
    public JsonResult AddTasks([FromBody]ToDoTask toDoTask)
    {
        db.ToDoTasks.Add(toDoTask);
        db.SaveChanges();
        return Json(Ok());
    }

    [HttpPost("DelTask")]
    public JsonResult DelTasks([FromBody]int id)
    {
        return Json(id);
            ToDoTask toDoTask = db.ToDoTasks.FirstOrDefault(p => p.Id == id);
            if (toDoTask != null)
            {
                db.ToDoTasks.Remove(toDoTask);
                db.SaveChanges();
                return Json(Ok());
            } 
            return Json(StatusCode(401));
        
    }

    [HttpPost("SelectTask")]
    public IActionResult SelectTasks(int id)
    {
        var a = db.ToDoTasks.Find(id);
        a.Selected =! a.Selected;
        return RedirectToAction("Index");
    }
    
}