namespace BlogProject.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T[]> GetAllAsync();
    Task<T> GetAsync(object id);
    Task CreateAsync(T item);
    void Update(T item);
    void Delete(T item);
}