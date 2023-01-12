namespace BlogProject.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T[]> GetAll();
    Task<T> Get(object id);
    Task Create(T item);
    void Update(T item);
    void Delete(T item);
}