namespace Tranee_Web_App.Models;

public class ToDoTask
{
    internal int Id { get; set; }
    public string TaskDescription { get; set; }
    internal bool Selected { get; set; }
    
    public ToDoTask(string taskDescription)
    {
        TaskDescription = taskDescription;
        Selected = false;
    }
}