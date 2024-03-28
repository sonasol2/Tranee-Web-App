using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoList : IToDoList
{
    private ApplicationContext db;
    public ToDoList(ApplicationContext context)
    {
        db = context;
    }
    public List<ToDoTask> AllTask(string userName)
    { 
        return db.ToDoTasks.Where(u => u.User.Name == userName).ToList();
    } 

    public void AddTask(ToDoTask task, int userId)
    {
        db.ToDoTasks.Add(new ToDoTask(){TaskDescription = task.TaskDescription, UserId = userId});
        db.SaveChanges();
    }
    
    // public void DelTask(int id)
    // {
    //     if (id != null)
    //     {
    //         ToDoTask toDoTask = db.ToDoTasks.FirstOrDefault(p => p.Id == id);
    //         if (toDoTask != null)
    //         {
    //             db.ToDoTasks.Remove(toDoTask);
    //             db.SaveChanges();
    //             // UpdateId();
    //             
    //         }
    //     }
    //     // _toDoList.RemoveAll(x => x.Selected);
    //     // UpdateId();
    // }
    
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