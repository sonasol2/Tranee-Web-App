using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using Microsoft.AspNetCore.Authorization;

namespace Tranee_Web_App;

[Route("/api/[controller]")]
[Authorize]
public class HomeController : Controller
{
    private ApplicationContext db;
    private ToDoList _toDoList;
    // private IToDoList test; какой способ будет правильный и вообще можно ли так делать.

    public HomeController(ApplicationContext context)
    {
        db = context;
        _toDoList = new ToDoList(context);
        // test = new ToDoList(context);
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        
        var userName = HttpContext.User.FindFirst("userName")?.Value;
        // return Ok(test.AllTask(userName));
        return Ok(_toDoList.AllTask(userName));
    }
    
    [HttpPut("add-task")]
    public IActionResult AddTasks([FromBody]ToDoTask toDoTask)
    {
        if (ModelState.IsValid)
        {
            var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
            if (!(userId != 0 & GetUserName() != null)) return BadRequest();
            _toDoList.AddTask(toDoTask, userId);
            return Ok(_toDoList.AllTask(GetUserName()));
        }
        return BadRequest();
    }

    [HttpDelete("del-task")]
    public IActionResult DelTasks([FromBody]int taskId)
    {
        if (GetUserName() == null) return BadRequest();
        if (_toDoList.DelTask(taskId))
        {
            return Ok(_toDoList.AllTask(GetUserName()));
        }
        return BadRequest();

    }
    
    [HttpPut("edit-task")]
    public IActionResult EditTasks([FromBody]string editDescription, int taskId)
    {
        if (!ModelState.IsValid) return Ok();
        if (GetUserId() != null & GetUserName() != null)
        {
            _toDoList.EditTask(editDescription, taskId);
            return Ok(_toDoList.AllTask(GetUserName()));
        }

        return Ok();
    }

    [HttpPost("select-task")]
    public IActionResult SelectTasks([FromBody] int taskId)
    {
        if (GetUserId() == null) return BadRequest();
        _toDoList.SelectTask(taskId);
        return Ok(_toDoList.AllTask(GetUserName()));

    }

    public string? GetUserName()
    {
        var userName = HttpContext.User.FindFirst("userName")?.Value;
        return userName ?? "Empty";
    }

    public object? GetUserId()
    {
        var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
        if (userId != 0)
        {
            return userId;
        }

        return null;
    }
    
}