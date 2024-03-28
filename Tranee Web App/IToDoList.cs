using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoList
{
    List<ToDoTask> AllTask(string userName);
    void AddTask(ToDoTask toDoTask, int userId);
    // void DelTask(int id);
    // void SelectTask(int index);
    // void UpdateId();
    //

}