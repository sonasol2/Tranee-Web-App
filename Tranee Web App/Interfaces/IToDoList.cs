using Tranee_Web_App.DTO;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoList
{
    IEnumerable<ToDoTaskDTO> AllTaskByUserName(string? userName);
    IEnumerable<ToDoTaskDTO> AllTaskById(int userId);
    Task AddTask(ToDoTaskDTO toDoTask, int userId);
    Task<bool> DelTask(int taskId);
    Task<bool> UpdateTask(string editDescription, int taskId);
    Task<bool> SelectTask(int taskId);

}