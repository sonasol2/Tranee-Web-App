namespace Tranee_Web_App.Models;

public class ToDoTask
{
    public int Id { get; set; }
    public string TaskDescription { get; set; }

    public ToDoTask(int id, string taskDescription)
    {
        Id = id;
        TaskDescription = taskDescription;
    }
}