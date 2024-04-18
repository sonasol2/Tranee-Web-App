using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;
using Microsoft.AspNetCore.Authorization;
using Tranee_Web_App.DTO;

namespace Tranee_Web_App;

[Route("/api/[controller]")]
[Authorize]
public class HomeController : Controller
{
    private IRepository<ToDoTask> _repository;
    private ApplicationContext db;
    private IToDoList _toDoList;

    public HomeController(ApplicationContext context, IToDoList toDoList, IRepository<ToDoTask> repository)
    {
        db = context;
        _toDoList = toDoList;
        _repository = repository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var userId = GetUserId();
        if (userId == null) return BadRequest();
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>());
        var mapper = new Mapper(config);
        var todoes = mapper.Map<List<ToDoTaskDTO>>(_toDoList.AllTaskById(userId.Value));
        return Ok(todoes);
    }
    
    [HttpPut("add-task")]
    public async Task<IActionResult> AddTasks([FromBody]ToDoTaskDTO toDoTask)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values);
        var userId = (int)GetUserId();
        var userName = GetUserName();
        if (!(userId != null & userName != null)) return BadRequest("Id or Name does not exist");
        await _toDoList.AddTask(toDoTask, userId);
        return Ok(_toDoList.AllTaskById(userId));
    }

    [HttpDelete("del-task")]
    public IActionResult DelTasks([FromBody]int taskId)
    {
        var userName = GetUserName();
        if (userName == null) return BadRequest("Name does not exist");
        var b = _toDoList.DelTask(taskId).Result;
        if (b) return Ok(_toDoList.AllTaskByUserName(userName));
        return BadRequest(ModelState.Values.ToString());

    }
    
    // [HttpPut("edit-task")]
    // public async Task<IActionResult> EditTasks([FromBody]string editDescription, int taskId)
    // {
    //     if (!ModelState.IsValid) return BadRequest(ModelState.Values.ToString());
    //     var userName = GetUserName();
    //     if (userName != null)
    //     {
    //         await _toDoList.UpdateTask(editDescription, taskId);
    //         return Ok(_toDoList.AllTaskByUserName(userName));
    //     }
    //
    //     return BadRequest("Name does not exist");
    // }
    
    [HttpPut("edit-task")]
    public async Task<IActionResult> EditTasks([FromBody]ToDoTaskDTO toDoTaskDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.ToString());
        var userName = GetUserName();
        if (userName != null)
        {
            _toDoList.UpdateTask(toDoTaskDto);
            return Ok(_toDoList.AllTaskByUserName(userName));
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
        return Ok(_toDoList.AllTaskByUserName(userName));

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