using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoListy : IToDoListy<ToDoListy>
{
    private IToDoAdder ToDoAdder { get; set; }
    private IToDoBinder ToDoBinder { get; set; }
    private IToDoRemover ToDoRemover { get; set; }
    private IToDoTaskReader ToDoTaskReader { get; set; }
    private IUserGetter UserGetter { get; set; }
    
    public ToDoListy(IToDoAdder toDoAdder, IToDoBinder toDoBinder, IToDoRemover toDoRemover, IToDoTaskReader toDoTaskReader, IUserGetter userGetter)
    {
        ToDoAdder = toDoAdder;
        ToDoBinder = toDoBinder;
        ToDoRemover = toDoRemover;
        ToDoTaskReader = toDoTaskReader;
        UserGetter = userGetter;
    }

    public void Process(ToDoTask toDoTask)
    {
        var userId = UserGetter.GetUserById();
        var taskDescription = ToDoTaskReader.GetInputDescription(toDoTask);
        ToDoAdder.AddToDo(taskDescription, userId);
    }
}