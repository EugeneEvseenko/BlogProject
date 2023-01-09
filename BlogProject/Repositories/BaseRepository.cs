using BlogProject.Database;
using BlogProject.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected DbContext _context;

    public DbSet<T> Set { get; private set; }

    public BaseRepository(ApplicationContext context)
    {
        _context = context;
        var set =_context.Set<T>();
        set.Load();

        Set = set;
    }

    public void Create(T item)
    {
        Set.Add(item);
        _context.SaveChanges();
    }

    public void Delete(T item)
    {
        Set.Remove(item);
        _context.SaveChanges();
    }

    public T Get(object id)
    {
        return Set.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return Set;
    }

    public void Update(T item)
    {
        Set.Update(item);
        _context.SaveChanges();
    }
}