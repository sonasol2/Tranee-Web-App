using System.ComponentModel.DataAnnotations;
using Tranee_Web_App.Models;

namespace Tranee_Web_App.DTO;

public class ToDoTaskDTO
{
    public int Id { get; set; }
        
    [Required(ErrorMessage = "Enter the task")]
    [StringLength(10, ErrorMessage = "Task length is so long, please enter ")]
    [RegularExpression($"/[A-Z, a-z]/g")]
    public string? TaskDescription { get; set; }
    public bool Selected { get; internal set; }
    // public int UserId { get; set; }
    //
    // public User? User { get; set; }
}