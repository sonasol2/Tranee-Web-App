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
    private ToDoListy _toDoListy;
    
    // private IToDoList test; какой способ будет правильный и вообще можно ли так делать.

    public HomeController(ApplicationContext context, ToDoListy toDoListy)
    {
        db = context;
        _toDoList = new ToDoList(context);
        _toDoListy = toDoListy;
        // test = new ToDoList(context);
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var userName = GetUserName();
        if (userName == null) return BadRequest("Name does not exist");
        // var userName = HttpContext.User.FindFirst("userName")?.Value;
        // return Ok(test.AllTask(userName));
        return Ok(_toDoList.AllTask(userName));
    }
    
    [HttpPut("add-task")]
    public async Task<IActionResult> AddTasks([FromBody]ToDoTask toDoTask)
    {
        _toDoListy.Process(toDoTask);
        // if (!ModelState.IsValid) return BadRequest(ModelState.Values);
        // var userId = GetUserId();
        // var userName = GetUserName();
        // // var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
        // if (!(userId != null & userName != null)) return BadRequest("Id or Name does not exist");
        // await _toDoList.AddTask(toDoTask, userId);
        // return Ok(_toDoList.AllTask(userName));
        return Ok();
    }

    [HttpDelete("del-task")]
    public IActionResult DelTasks([FromBody]int taskId)
    {
        var userName = GetUserName();
        if (userName == null) return BadRequest("Name does not exist");
        
        if (_toDoList.DelTask(taskId).Result) return Ok(_toDoList.AllTask(userName));
        
        return BadRequest(ModelState.Values.ToString());

    }
    
    [HttpPut("edit-task")]
    public async Task<IActionResult> EditTasks([FromBody]string editDescription, int taskId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.ToString());
        var userName = GetUserName();
        if (userName != null)
        {
            await _toDoList.EditTask(editDescription, taskId);
            return Ok(_toDoList.AllTask(userName));
        }

        return BadRequest("Name does not exist");
    }

    [HttpPost("select-task")]
    public async Task<IActionResult> SelectTasks([FromBody] int taskId)
    {
        var userId = GetUserId();
        var userName = GetUserName();
        if (userId == null & userName != null) return BadRequest("Id or Name does not exist");
        await _toDoList.SelectTask(taskId);
        return Ok(_toDoList.AllTask(userName));

    }

    public string? GetUserName()
    {
        var userName = HttpContext.User.FindFirst("userName")?.Value;
        return userName ?? null;
    }

    public int? GetUserId()
    {
        var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
        if (userId != 0) return userId;
        return null;
    }
    
}