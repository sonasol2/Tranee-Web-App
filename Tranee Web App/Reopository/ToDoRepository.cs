using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;
public class ToDoRepository : IRepository<ToDoTask>
{
    private readonly ApplicationContext _db;
    private readonly IMemoryCache _cache;
    private readonly ILogger<ToDoRepository> _logger;
    private bool disposed = false;

    public ToDoRepository(ApplicationContext context, IMemoryCache cache, ILogger<ToDoRepository> logger)
    {
        _db = context;
        _cache = cache;
        _logger = logger;
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    public IEnumerable<ToDoTask> GetAllTaskById(int userId) // Необходимо дописать лолгику кеширования для удаления и добавления новый тасок
    {
        DateTime timeNow = DateTime.Now; // надо подумать как это все переделать
        var a = _cache.TryGetValue(userId, out List<ToDoTask>? toDoTask);
        _logger.LogInformation($"{timeNow}Удалось ли получить данные из кеша:{a}");
        if (toDoTask == null)
        {
            var toDoTasks = _db.ToDoTasks.Where(u => u.User.Id == userId).ToList();
            if (toDoTasks != null)
            {
                _cache.Set(userId, toDoTasks,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                _logger.LogInformation($"{timeNow}Данные внесены в кеш");
            }
            _logger.LogInformation($"{timeNow}Взяли только что кешированные данные");
            return toDoTasks;
            
        }
        _logger.LogInformation($"{timeNow}Взяли старые кешированные данные");
        return toDoTask;
    }
    
    
    public IEnumerable<ToDoTask> GetAllTaskByName(string? userName)
    {
        return _db.ToDoTasks.Where(u => u.User.Name == userName).ToList();
    }

    public async Task<ToDoTask> Get(int id)
    {
        return await _db.ToDoTasks.FindAsync(id);
    }

    public async Task CreateAsync(ToDoTask item, int id)
    {
        await _db.ToDoTasks.AddAsync(new ToDoTask(){TaskDescription = item.TaskDescription, UserId = id});
    }

    public async Task Update(ToDoTask item)
    { 
        _db.Entry(item).State = EntityState.Modified;
    }

    public async Task Delete(int id)
    {
        var toDoTask = await TaskSearcherAsync(id);
        if (toDoTask != null) _db.ToDoTasks.Remove(toDoTask);
    }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
    
    public async Task<ToDoTask> TaskSearcherAsync(int taskId)
    {
        var task = await _db.ToDoTasks.FirstOrDefaultAsync(t => t.Id == taskId);
        if (task != null)
        {
            return task;
        }
        return null;
    }
    
    public async Task<ToDoTask> TaskSearcherAsync(string userName)
    {
        var task = await _db.ToDoTasks.FirstOrDefaultAsync(t => t.User.Name == userName);
        if (task != null)
        {
            return task;
        }
        return null;
    }
}