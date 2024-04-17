namespace Tranee_Web_App;

public interface IRepository<T> : IDisposable where T : class
{
    IEnumerable<T> GetAllTaskById(int id);
    IEnumerable<T> GetAllTaskByName(string? id);
    T Get(int id);
    void Create(T item, int id);
    void Update(T item);
    void Delete(int id);
    void Save();

}