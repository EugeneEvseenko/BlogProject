using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogProject.Classes;
using BlogProject.Models.API.Response;
using BlogProject.Models.Database.Users;
using BlogProject.Models.ViewModels;
using BlogProject.Models.ViewModels.Users;
using BlogProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers;

/// <summary>
/// Управление пользователями
/// </summary>
[Route("api/user")]
[ApiController]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    private readonly IRandomService _randomService;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public UserController(
        IUserService userService, 
        IMapper mapper, 
        SignInManager<User> signInManager, 
        UserManager<User> userManager,
        IRandomService randomService)
    {
        _userService = userService;
        _mapper = mapper;
        _signInManager = signInManager;
        _userManager = userManager;
        _randomService = randomService;
    }

    /// <summary>
    /// Создание рандомных пользователей определенного количества
    /// </summary>
    /// <param name="count">Количество пользователей</param>
    [HttpPost]
    [Route("generate")]
    [ProducesDefaultResponseType(typeof(BaseResponse<GenerateUsersViewModel>))]
    public async Task<IActionResult> Generate([FromQuery] int count)
    {
        if (count > 0)
        {
            var listUsers = new Dictionary<string, string>();
            foreach (var i in Enumerable.Range(0, count))
            {
                var user = new User()
                {
                    FirstName = _randomService.GetRandomName(),
                    LastName = _randomService.GetRandomLastName()
                };
                user.Email = $"{user.FirstName.ToLower()}_{user.LastName.ToLower()}@generated.com";
                user.UserName = user.Email;
                listUsers.Add($"{user.FirstName} {user.LastName}", user.UserName);
                await _userManager.CreateAsync(user, $"{user.FirstName}{user.LastName}");
            }
                
            return ApiResult(true, data:
                new GenerateUsersViewModel()
            {
                UsersAdded = listUsers.Count,
                Users = listUsers
            });
        }
        return ApiResult(false);
    }

    /// <summary>
    /// Получение списка всех пользователей
    /// </summary>
    [HttpGet]
    [Route("get-all")]
    [ProducesDefaultResponseType(typeof(BaseResponse<User[]>))]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsers();
        return ApiResult(true, data:users);
    }

    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="guid">Идентификатор пользователя</param>
    [HttpGet]
    [Route("get")]
    [ProducesDefaultResponseType(typeof(BaseResponse<User>))]
    public async Task<IActionResult> Get([FromQuery] string guid)
    {
        var user = await _userService.GetUser(guid);
        return ApiResult(user != null,  data: user);
    }

    /// <summary>
    /// Добавление нового пользователя
    /// </summary>
    [HttpPost]
    [Route("add")]
    [ProducesDefaultResponseType(typeof(BaseResponse))]
    public async Task<IActionResult> Post([FromBody] RegisterViewModel register)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(register);
            var result = await _userManager.CreateAsync(user, register.PasswordReg);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return ApiResult(result.Succeeded);
        } 
        return ApiResult(false);
    }

    /// <summary>
    /// Обновление существующего пользователя
    /// </summary>
    /// <param name="user"></param>
    [HttpPut("update")]
    [ProducesDefaultResponseType(typeof(BaseResponse))]
    public async Task<IActionResult> Put([FromBody] User user)
    {
        var result = await _userService.UpdateUser(user);
        return result ? Ok() : BadRequest();
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("delete")]
    [ProducesDefaultResponseType(typeof(BaseResponse))]
    public async Task<IActionResult> Delete([FromQuery] string guid)
    {
        var result = await _userService.DeleteUser(guid);
        return ApiResult(result);
    }
        
    /// <summary>
    /// Удаление всех пользователей
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("delete-all")]
    [ProducesDefaultResponseType(typeof(BaseResponse<int>))]
    public async Task<IActionResult> DeleteAll()
    {
        var result = await _userService.DeleteAllUsers();
        return ApiResult(result > 0, data: result);
    }
}