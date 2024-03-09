using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoList : IToDoList
{
    private List<ToDoTask> _toDoList = new List<ToDoTask>();

    public IEnumerable<string> AllTask()
    {
        return _toDoList.Select(x => $"{x.Id}. {x.TaskDescription} Is Selected:{x.Selected}" );
    } 

    public void AddTask(ToDoTask task)
    {
        _toDoList.Add(new ToDoTask(task.TaskDescription));
        _toDoList.Last().Id = _toDoList.Count;
    }
    
    public void DelTask()
    {
            _toDoList.RemoveAll(x => x.Selected);
            UpdateId();
    }
    
    public void UpdateId()
    {
        for (int i = 0; i < _toDoList.Count; i++)
        {
            _toDoList[i].Id = i+1;
        }
    }
    
    public void SelectTask(int index)
    {
        if (_toDoList[index].Selected == false)
        {
            _toDoList[index].Selected = true;
        }
        else _toDoList[index].Selected = false;
        
    }
    
}