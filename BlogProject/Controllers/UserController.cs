using BlogProject.Models.Database.Users;
using BlogProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers;

[Route("users")]
public class UserController : Controller
{
    private readonly IPostsRepository _postsRepository;
    [HttpGet]
    [Route("create")]
    public IActionResult CreateUser(User user)
    {
        return Ok();
    }
}