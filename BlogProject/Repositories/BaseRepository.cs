using BlogProject.Database;
using BlogProject.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected DbContext _context;

    public DbSet<T> _currentSet { get; private set; }

    public BaseRepository(ApplicationContext context)
    {
        _context = context;
        var set =_context.Set<T>();
        set.Load();

        _currentSet = set;
    }

    public void Create(T item)
    {
        _currentSet.Add(item);
        _context.SaveChanges();
    }

    public void Delete(T item)
    {
        _currentSet.Remove(item);
        _context.SaveChanges();
    }

    public T Get(object id)
    {
        return _currentSet.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _currentSet;
    }

    public void Update(T item)
    {
        _currentSet.Update(item);
        _context.SaveChanges();
    }
}