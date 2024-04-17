using Tranee_Web_App.Models;

namespace Tranee_Web_App.DTO;

public class ToDoTaskDTO
{
    
    public string? TaskDescription { get; set; }
    public bool Selected { get; internal set; }
    // public int UserId { get; set; }
    //
    // public User? User { get; set; }
}