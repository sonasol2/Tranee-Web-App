using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoList
{
    IEnumerable<string> GetTask();
    void SetTask(ToDoTask toDoTask);
}