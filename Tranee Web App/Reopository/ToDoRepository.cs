using Microsoft.EntityFrameworkCore;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;
public class ToDoRepository : IRepository<ToDoTask>
{
    private ApplicationContext db;
    private bool disposed = false;

    public ToDoRepository(ApplicationContext context)
    {
        db = context;
    }
    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    public IEnumerable<ToDoTask> GetAllTaskById(int userId)
    {
        return db.ToDoTasks.Where(u => u.User.Id == userId).ToList();
    }
    
    
    public IEnumerable<ToDoTask> GetAllTaskByName(string? userName)
    {
        return db.ToDoTasks.Where(u => u.User.Name == userName).ToList();
    }

    public ToDoTask Get(int id)
    {
        return db.ToDoTasks.Find(id);
    }

    public void Create(ToDoTask item, int id)
    {
        db.ToDoTasks.Add(new ToDoTask(){TaskDescription = item.TaskDescription, UserId = id});
    }

    public void Update(ToDoTask item)
    {
        db.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        var toDoTask = db.ToDoTasks.FirstOrDefault(t=> t.Id == id);
        if (toDoTask != null) db.ToDoTasks.Remove(toDoTask);
    }

    public void Save()
    {
        db.SaveChanges();
    }
    
    public async Task<ToDoTask> TaskSearcher(int taskId)
    {
        var task = await db.ToDoTasks.FirstOrDefaultAsync(t => t.Id == taskId);
        if (task != null)
        {
            return task;
        }
        return null;
    }
    
    public async Task<ToDoTask> TaskSearcher(string userName)
    {
        var task = await db.ToDoTasks.FirstOrDefaultAsync(t => t.User.Name == userName);
        if (task != null)
        {
            return task;
        }
        return null;
    }
}