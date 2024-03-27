using Microsoft.EntityFrameworkCore;

namespace Tranee_Web_App.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    // public int ToDoTaskId { get; set; }
    // public ToDoTask? ToDoTask { get; set; }
    public List<ToDoTask> ToDoTasks { get; set; } = new();
    
    
    // public User(string name, string password)
    // {
    //     Name = name;
    //     Password = password;
    // }
    //
    // public User()
    // {
    //     
    // }
}