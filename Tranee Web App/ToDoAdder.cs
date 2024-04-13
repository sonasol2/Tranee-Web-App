using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoAdder : IToDoAdder
{
    private ApplicationContext db;

    public ToDoAdder(ApplicationContext context)
    {
        db = context;
    }
    
    public async Task AddToDo(string? taskDescription, int userId)
    {
        await db.ToDoTasks.AddAsync(new ToDoTask()
            { TaskDescription = taskDescription, UserId = userId });
        await db.SaveChangesAsync();
    }
}