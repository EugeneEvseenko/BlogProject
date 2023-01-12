using BlogProject.Database;
using BlogProject.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Repositories.Impl;

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

    public async Task Create(T item)
    {
        await _currentSet.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public void Delete(T item)
    {
        _currentSet.Remove(item);
        _context.SaveChanges();
    }

    public async Task<T> Get(object id)
    {
        return await _currentSet.FindAsync(id);
    }

    public async Task<T[]> GetAll()
    {
        return await _currentSet.ToArrayAsync();
    }

    public void Update(T item)
    {
        _currentSet.Update(item);
        _context.SaveChanges();
    }
}