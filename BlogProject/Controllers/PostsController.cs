using AutoMapper;
using BlogProject.Models.Database.Posts;
using BlogProject.Models.Database.Users;
using BlogProject.Services.Impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers;

/// <summary>
/// Управление пользователями
/// </summary>
[Route("api/posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IMapper _mapper;

    public PostsController(UserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получение поста по идентификатору
    /// </summary>
    /// <param name="guid">Идентификатор поста пользователя</param>
    [HttpGet]
    [Route("get")]
    [ProducesDefaultResponseType(typeof(Post))]
    public async Task<IActionResult> Get([FromQuery] string guid)
    {
        var user = await _userService.GetUser(guid);
        return user != null ? Ok(user) : NotFound();
    }
}