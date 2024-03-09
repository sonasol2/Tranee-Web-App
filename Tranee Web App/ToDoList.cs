using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoList : IToDoList
{
    private List<ToDoTask> _toDoList = new List<ToDoTask>();
    
    public IEnumerable<string> GetTask()
    {
        return _toDoList.Select(x => $"{x.Id}. {x.TaskDescription}" );
    } 

    public void SetTask(ToDoTask task)
    {
        _toDoList.Add(new ToDoTask(task.Id,task.TaskDescription));
    }

}