using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tranee_Web_App.Models
    ;
using Task = System.Threading.Tasks.Task;

namespace Tranee_Web_App;

[Route("[controller]")]
public class HomeController : Controller
{
    readonly IToDoList _toDoList;

    public HomeController(IToDoList toDoLi)
    {
        _toDoList = toDoLi;
    }
    [HttpGet]
    public IActionResult GetTasks()
    {
        return Ok(_toDoList.AllTask());
    }
    
    [HttpPost("SetTask")]
    public IActionResult SetTasks([FromBody]ToDoTask toDoTask)
    {
        _toDoList.AddTask(toDoTask);
        return Ok();
    }

    [HttpPost("DelTask")]
    public IActionResult DelTasks()
    {
        _toDoList.DelTask();
        return Ok();
    }

    [HttpPost("SelectTask")]
    public IActionResult SelectTasks(int index)
    {
        _toDoList.SelectTask(index);
        return Ok();
    }

    // [HttpPost("MasDelete")]
    // public IActionResult MasDelTasks()
    // {
    //     _toDoList.MasDelTask();
    //     return Ok();
    // }
}