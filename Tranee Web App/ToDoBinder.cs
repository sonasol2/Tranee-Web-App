using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoBinder : IToDoBinder
{
    public ToDoTask AddTask(ToDoTask toDoTask)
    {
        if (toDoTask.TaskDescription.Length >= 1000)
        {
            return new ToDoTask(){};
        }

        return null;
    }
}