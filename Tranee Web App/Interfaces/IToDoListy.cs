using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoListy<T> where T: class, IToDoListy<T>
{
    void Process(ToDoTask toDoTask);
}