using BlogProject.Database;
using BlogProject.Models.Database.Posts;
using BlogProject.Models.Database.Users;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Repositories.Impl;

public class PostsRepository : BaseRepository<Post>, IPostsRepository
{

    public PostsRepository(ApplicationContext context) : base(context) { }

    public async Task<Post[]> GetUserPosts(User user)
    {
        return await GetUserPosts(user.Id);
    }

    public async Task<Post[]> GetUserPosts(string id)
    {
        return await _currentSet.Where(x => x.User.Id == id).ToArrayAsync();
    }
}