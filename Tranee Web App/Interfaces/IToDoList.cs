using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoList
{
    List<ToDoTask> AllTask(string userName);
    Task AddTask(ToDoTask toDoTask, int? userId);
    Task<bool> DelTask(int taskId);
    Task<bool> EditTask(string editDescription, int taskId);
    Task<bool> SelectTask(int taskId);

}