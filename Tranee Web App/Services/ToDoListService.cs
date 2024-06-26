using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Tranee_Web_App.DTO;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ToDoListService : IToDoListService
{
    private IRepository<ToDoTask> _repository;
    private IMemoryCache _cache;
    
    public ToDoListService(ApplicationContext context, IRepository<ToDoTask> repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }
    
    public IEnumerable<ToDoTaskDTO> AllTaskByUserName(string? userName)
    {
        
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>());
        var mapper = new Mapper(config);
        var todoes = mapper.Map<List<ToDoTaskDTO>>(_repository.GetAllTaskByName(userName));
        return todoes;
    } 
    
    public IEnumerable<ToDoTaskDTO> AllTaskById(int id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>());
        var mapper = new Mapper(config);
        var todoes = mapper.Map<List<ToDoTaskDTO>>(_repository.GetAllTaskById(id));
        return todoes;
    } 
    
    public async Task AddTask(ToDoTaskDTO task, int userId)
    {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTask>()
                // .ForMember("", opt => opt.MapFrom(c => c.))
                );
            var mapper = new Mapper(config);
            ToDoTask toDoTask = mapper.Map<ToDoTaskDTO, ToDoTask>(task);
            _repository.CreateAsync(toDoTask, userId);
            _repository.Save(); 
    }
    
    public async Task<bool> DelTask(int taskId)
    {
        var task = _repository.TaskSearcherAsync(taskId);
        
        if (taskId != null)
        {
            _repository.Delete(taskId);
            _repository.Save();
            return true;
        }
        return false;
    }
    
    public async Task<bool> UpdateTask(ToDoTaskDTO toDoTaskDto)
    {
        var task = await _repository.TaskSearcherAsync(toDoTaskDto.Id);
        
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
        var task = _repository.TaskSearcherAsync(taskId).Result;
        
        if (task != null)
        {
            task.Selected = !task.Selected;
            _repository.Save();
            return true;
        }
        return false;
    }
    
}