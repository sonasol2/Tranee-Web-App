using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Tranee_Web_App.Models;

public class User : BaseUser
{
    [Required (ErrorMessage = "Please enter the name")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "The password should be between 3 and 30 letters in length")]
    public string? Password { get; set; }

    public List<ToDoTask>? ToDoTasks { get; set; } = new();
    
    public Role Role { get; set; }
    
    public int RoleId { get; set; }
}