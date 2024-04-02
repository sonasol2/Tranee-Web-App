using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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

    public async Task AddTask(ToDoTask task, int? userId)
    {
        if (userId != null)
        {
            var id = userId.Value;
            await db.ToDoTasks.AddAsync(new ToDoTask(){TaskDescription = task.TaskDescription, UserId = id});
            await db.SaveChangesAsync();
        }
    }
    
    public async Task<bool> DelTask(int taskId)
    {
        var task = TaskSearcher(taskId).Result;
        if (task != null)
        {
            db.ToDoTasks.Remove(task);
            await db.SaveChangesAsync();
            return true;
        }
        return false;
    }
    
    public async Task<bool> EditTask(string editDescription, int taskId)
    {
        var task = TaskSearcher(taskId).Result;
        if (task != null)
        {
            task.TaskDescription = editDescription;
            db.ToDoTasks.Update(task);
            await db.SaveChangesAsync();
            return true;
        }
        return false;
    }
    
    public async Task<bool> SelectTask(int taskId)
    {
        var task = TaskSearcher(taskId).Result;
        if (task != null)
        {
            task.Selected = !task.Selected;
            await db.SaveChangesAsync();
            return true;
        }
        return false;
    }
        
    private async Task<ToDoTask> TaskSearcher(int taskId)
    {
        var task = await db.ToDoTasks.FirstOrDefaultAsync(t => t.Id == taskId);
        if (task != null)
        {
            return task;
        }
        return null;
    }
    
    private async Task<ToDoTask> TaskSearcher(string userName)
    {
        var task = await db.ToDoTasks.FirstOrDefaultAsync(t => t.User.Name == userName);
        if (task != null)
        {
            return task;
        }
        return null;
    }
}