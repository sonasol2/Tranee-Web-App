using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoList
{
    List<ToDoTask> AllTask(string userName);
    void AddTask(ToDoTask toDoTask, int userId);
    bool DelTask(int taskId);
    void EditTask(string editDescription, int taskId);
    bool SelectTask(int taskId);

}