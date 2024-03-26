namespace Tranee_Web_App.Models;

public class ToDoTask
{
    public int Id { get; set; }
    public string TaskDescription { get; set; }
    public bool Selected { get; internal set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public ToDoTask(string taskDescription)
    {
        TaskDescription = taskDescription;
        Selected = false;
    }

    public ToDoTask()
    {
        
    }
}