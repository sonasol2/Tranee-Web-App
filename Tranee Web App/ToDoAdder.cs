using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoAdder : IToDoAdder
{
    private ApplicationContext db;
    public IUserGetter UserGet { get;}
    public IToDoTaskReader ToDoTaskReader { get; }

    public ToDoAdder(ApplicationContext context, IUserGetter userGet, IToDoTaskReader toDoTaskReader)
    {
        db = context;
        UserGet = userGet;
        ToDoTaskReader = toDoTaskReader;
    }
    
    public async Task AddToDo(ToDoTask toDoTask)
    {
        await db.ToDoTasks.AddAsync(new ToDoTask()
            { TaskDescription = ToDoTaskReader.GetInputDescription(toDoTask), UserId = UserGet.GetUserById() });
        await db.SaveChangesAsync();
    }
}