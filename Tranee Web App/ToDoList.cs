using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoList : IToDoList
{
    private List<ToDoTask> _toDoList = new List<ToDoTask>();
    private ApplicationContext db;
    public IEnumerable<string> AllTask()
    {
        return _toDoList.Select(x => $"{x.Id}. {x.TaskDescription} Is Selected:{x.Selected}" );
    } 

    public void AddTask(ToDoTask task)
    {
        // _toDoList.Add(new ToDoTask(task.TaskDescription));
        db.ToDoTasks.Add(new ToDoTask(task.TaskDescription));
        // _toDoList.Last().Id = _toDoList.Count;
        // db.ToDoTasks.Last().Id = db.ContextId();
    }
    
    public void DelTask(int id)
    {
        if (id != null)
        {
            ToDoTask toDoTask = db.ToDoTasks.FirstOrDefault(p => p.Id == id);
            if (toDoTask != null)
            {
                db.ToDoTasks.Remove(toDoTask);
                db.SaveChanges();
                // UpdateId();
                
            }
        }
        // _toDoList.RemoveAll(x => x.Selected);
        // UpdateId();
    }
    
    // public void UpdateId()
    // {
    //     
    //     for (int i = 0; i < db.ToDoTasks.Count(); i++)
    //     {
    //         // _toDoList[i].Id = i+1;
    //         db.ToDoTasks.Find(i).Id = i + 1;
    //     }
    //     db.SaveChanges();
    // }
    
    // public void SelectTask(int index)
    // {
            // _toDoList[index].Selected =!_toDoList[index].Selected;
            //     if (_toDoList[index].Selected == false)
            //     {
            //         _toDoList[index].Selected = true;
            //     }
            //     else _toDoList[index].Selected = false;
            //     
            // }

}