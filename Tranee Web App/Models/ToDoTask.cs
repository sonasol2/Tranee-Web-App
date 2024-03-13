namespace Tranee_Web_App.Models;

public class ToDoTask
{
    public int Id { get; set; }
    public string TaskDescription { get; set; }
    public bool Selected { get; internal set; }

    // public List<User> Users { get; set; } = new();
    public ToDoTask(string taskDescription)
    {
        TaskDescription = taskDescription;
        Selected = false;
    }

    public ToDoTask()
    {
        
    }
}