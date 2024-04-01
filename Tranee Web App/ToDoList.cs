using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoList : IToDoList
{
    private ApplicationContext db;
    public ToDoList(ApplicationContext context)
    {
        db = context;
    }
    public List<ToDoTask> AllTask(string? userName)
    {
        return db.ToDoTasks.Where(u => u.User.Name == userName).ToList();
    } 

    public void AddTask(ToDoTask task, int userId)
    {
        db.ToDoTasks.Add(new ToDoTask(){TaskDescription = task.TaskDescription, UserId = userId});
        db.SaveChanges();
    }
    
    public bool DelTask(int taskId)
    {
        var task = TaskSearcher(taskId);
        if (task != null)
        {
            db.ToDoTasks.Remove(task);
            db.SaveChanges();
            return true;
        }
        return false;
    }
    
    public void EditTask(string editDescription, int taskId)
    {
        var task = TaskSearcher(taskId);
        task.TaskDescription = editDescription;
        db.SaveChanges();
    }
    
    public bool SelectTask(int taskId)
    {
        var task = TaskSearcher(taskId);
        if (task != null)
        {
            task.Selected = !task.Selected;
            db.SaveChanges();
            return true;
        }
        return false;
    }
        
    private ToDoTask TaskSearcher(int taskId)
    {
        var task = db.ToDoTasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            return task;
        }
        return null;
    }
    
    private ToDoTask TaskSearcher(string userName)
    {
        var task = db.ToDoTasks.FirstOrDefault(t => t.User.Name == userName);
        if (task != null)
        {
            return task;
        }
        return null;
    }
}