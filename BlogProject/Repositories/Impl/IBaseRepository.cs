namespace BlogProject.Repositories.Impl;

public interface IBaseRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T Get(object id);
    void Create(T item);
    void Update(T item);
    void Delete(T item);
}