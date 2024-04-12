using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoListy
{
    private IToDoAdder ToDoAdder { get; set; }
    private IToDoBinder ToDoBinder { get; set; }
    private IToDoRemover ToDoRemover { get; set; }
    
    public ToDoListy(IToDoAdder toDoAdder, IToDoBinder toDoBinder, IToDoRemover toDoRemover)
    {
        ToDoAdder = toDoAdder;
        ToDoBinder = toDoBinder;
        ToDoRemover = toDoRemover;
    }

    public void Process(ToDoTask toDoTask)
    {
        ToDoAdder.AddToDo(toDoTask);
    }
}