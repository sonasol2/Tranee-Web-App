using Tranee_Web_App.Models;

namespace Tranee_Web_App;


public interface IToDoBinder
{
    ToDoTask AddTask(ToDoTask toDoTask);
}