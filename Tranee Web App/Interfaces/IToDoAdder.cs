using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoAdder
{
    Task AddToDo(ToDoTask toDoTask);
}