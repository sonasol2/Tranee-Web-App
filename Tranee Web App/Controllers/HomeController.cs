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
        return Ok(_toDoList.GetTask());
    }
    
    [HttpPost]
    public IActionResult SetTasks([FromBody]ToDoTask toDoTask)
    {
        _toDoList.SetTask(toDoTask);
        return Ok();
    }
}