using BlogProject.Repositories;

namespace BlogProject.Services.Impl;

public class PostsService
{
    private readonly IPostsRepository _postsRepository;

    public PostsService(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }

    public async Task<bool> CreatePost()
    {

        return false;
    }
}