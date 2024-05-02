using Tranee_Web_App.DTO;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoListService
{
    IEnumerable<ToDoTaskDTO> AllTaskByUserName(string? userName);
    IEnumerable<ToDoTaskDTO> AllTaskById(int userId);
    Task AddTask(ToDoTaskDTO toDoTask, int userId);
    Task<bool> DelTask(int taskId);
    Task<bool> UpdateTask(ToDoTaskDTO toDoTaskDto);
    Task<bool> SelectTask(int taskId);

}