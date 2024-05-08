using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tranee_Web_App.Models;

public class ToDoTask
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Enter the task")]
    [StringLength(10, ErrorMessage = "Task length is so long, please enter ")]
    public string? TaskDescription { get; set; }
    public bool Selected { get; internal set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }
    
    public DateTime? DateTime { get; set; }
    

}