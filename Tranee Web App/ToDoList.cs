using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Tranee_Web_App.DTO;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoList : IToDoList
{
    private ApplicationContext db;
    private IRepository<ToDoTask> _repository;
    public ToDoList(ApplicationContext context, IRepository<ToDoTask> repository)
    {
        db = context;
        _repository = repository;
    }
    public IEnumerable<ToDoTaskDTO> AllTaskByUserName(string? userName)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>());
        var mapper = new Mapper(config);
        var todoes = mapper.Map<List<ToDoTaskDTO>>(_repository.GetAllTaskByName(userName));
        // return db.ToDoTasks.Where(u => u.User.Name == userName).ToList();
        return todoes;
    } 
    
    public IEnumerable<ToDoTaskDTO> AllTaskById(int id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>());
        var mapper = new Mapper(config);
        var todoes = mapper.Map<List<ToDoTaskDTO>>(_repository.GetAllTaskById(id));
        // return db.ToDoTasks.Where(u => u.User.Id == id).ToList();
        return todoes;
    } 
    public async Task AddTask(ToDoTaskDTO task, int userId)
    {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTask>()
                // .ForMember("", opt => opt.MapFrom(c => c.))
                );
            var mapper = new Mapper(config);
            ToDoTask toDoTask = mapper.Map<ToDoTaskDTO, ToDoTask>(task);
            // var id = userId.Value;
            // await db.ToDoTasks.AddAsync(new ToDoTask(){TaskDescription = task.TaskDescription, UserId = id});
            _repository.Create(toDoTask, userId);
            _repository.Save();
            // await db.SaveChangesAsync();
    }
    
    public async Task<bool> DelTask(int taskId)
    {
        var task = await TaskSearcher(taskId);
        if (taskId != null)
        {
            _repository.Delete(taskId);
            _repository.Save();
            // db.ToDoTasks.Remove(task);
            // await db.SaveChangesAsync();
            return true;
        }
        return false;
    }
    
    public async Task<bool> UpdateTask(ToDoTaskDTO toDoTaskDto)
    {
        var task = await TaskSearcher(toDoTaskDto.Id);
        if (task != null)
        {
            task.TaskDescription = toDoTaskDto.TaskDescription;
            _repository.Update(task);
            _repository.Save();
            return true;
        }
        return false;
    }
    
    public async Task<bool> SelectTask(int taskId)
    {
        var task = TaskSearcher(taskId).Result;
        if (task != null)
        {
            task.Selected = !task.Selected;
            await db.SaveChangesAsync();
            return true;
        }
        return false;
    }
        
    private async Task<ToDoTask> TaskSearcher(int taskId)
    {
        var task = await db.ToDoTasks.FirstOrDefaultAsync(t => t.Id == taskId);
        if (task != null)
        {
            return task;
        }
        return null;
    }
    
    private async Task<ToDoTask> TaskSearcher(string userName)
    {
        var task = await db.ToDoTasks.FirstOrDefaultAsync(t => t.User.Name == userName);
        if (task != null)
        {
            return task;
        }
        return null;
    }
}