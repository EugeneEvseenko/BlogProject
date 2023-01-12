using BlogProject.Database;
using BlogProject.Models.Database.Posts;
using BlogProject.Models.Database.Users;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Repositories.Impl;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context) { }

    
}