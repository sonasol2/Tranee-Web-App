using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoTaskReader : IToDoTaskReader
{
    public string? GetInputDescription(ToDoTask toDoTask)
    {
        if (toDoTask.TaskDescription != null)
        {
            return toDoTask.TaskDescription;
        }
        return null;
    }
}