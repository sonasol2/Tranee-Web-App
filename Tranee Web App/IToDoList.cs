using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoList
{
    IEnumerable<string> AllTask();
    void AddTask(ToDoTask toDoTask);
    void DelTask(int id);
    // void SelectTask(int index);
    // void UpdateId();
  

}