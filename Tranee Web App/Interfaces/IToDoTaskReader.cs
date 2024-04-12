using Microsoft.AspNetCore.Mvc;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public interface IToDoTaskReader
{
    string? GetInputDescription(ToDoTask toDoTask);
}