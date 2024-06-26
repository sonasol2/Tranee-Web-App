using System.Security.Claims;
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
    
    private readonly IToDoListService _toDoListService;

    public HomeController(IToDoListService toDoListService)
    {
        _toDoListService = toDoListService;
    }
    
    [HttpGet]
    public IActionResult Index() // получается следуюя принципам restApi необходимо поменять так чтобы здесь ничего не изменялось 
    {
        int userId = GetUserId();
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>());
        var mapper = new Mapper(config);
        var todoes = mapper.Map<List<ToDoTaskDTO>>(_toDoListService.AllTaskById(userId));

        return Ok(todoes);
        
    }
    
    [HttpPut("add-task")]
    public async Task<IActionResult> AddTasks([FromBody]ToDoTaskDTO toDoTask)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values);
        var userId = (int)GetUserId(); // слабое место - может прийти null и рабоать не будет.
        if (!(userId != null )) return BadRequest("ID does not exist");
        await _toDoListService.AddTask(toDoTask, userId);
        return Ok(_toDoListService.AllTaskById(userId));
    }

    [HttpDelete("del-task")]
    public IActionResult DelTasks([FromBody]int taskId)
    {
        var userName = GetUserName(); //some var
        if (userName == null) return BadRequest("Name does not exist");
        var b = _toDoListService.DelTask(taskId).Result;
        if (b) return Ok(_toDoListService.AllTaskByUserName(userName));
        return BadRequest(ModelState.Values.ToString());

    }
    
    [HttpPut("edit-task")]
    public async Task<IActionResult> EditTasks([FromBody]ToDoTaskDTO toDoTaskDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.ToString());
        var userName = GetUserName();
        if (userName != null)
        {
            await _toDoListService.UpdateTask(toDoTaskDto);
            return Ok(_toDoListService.AllTaskByUserName(userName));
        }

        return BadRequest("Name does not exist");
    }
    
    [HttpPost("select-task")]
    public async Task<IActionResult> SelectTasks([FromBody] int taskId)
    {
        var userId = GetUserId();
        var userName = GetUserName();
        if (userId == null & userName != null) return BadRequest("Id or Name does not exist");
        await _toDoListService.SelectTask(taskId);
        return Ok(_toDoListService.AllTaskByUserName(userName));

    }

    [HttpGet("admin-panel")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminPanel()
    {
        return Ok("Admin Panel Ok");
    }
    
    public string? GetUserName()
    {
        var userName = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        return userName ?? null;
    }

    public int GetUserId()
    {
        var userId = int.Parse(HttpContext.User.FindFirst("userId")?.Value);
        if (userId != 0) return userId;
        return 0;
    }
    
}