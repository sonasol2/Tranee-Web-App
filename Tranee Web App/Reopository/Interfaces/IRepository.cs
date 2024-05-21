namespace Tranee_Web_App;

public interface IRepository<T> : IDisposable where T : class
{
    IEnumerable<T> GetAllTaskById(int id);
    IEnumerable<T> GetAllTaskByName(string? id);
    Task<T> Get(int id);
    Task CreateAsync(T item, int id);
    Task Update(T item);
    Task Delete(int id);
    Task Save();
    Task<T> TaskSearcherAsync(string userName);
    Task<T> TaskSearcherAsync(int userId);
}