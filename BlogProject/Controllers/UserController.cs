using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProject.Models.Database.Users;
using BlogProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Route("get-all")]
        [ProducesDefaultResponseType(typeof(User[]))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        // GET: api/User/5
        [HttpGet]
        [Route("get")]
        [ProducesDefaultResponseType(typeof(User))]
        public async Task<IActionResult> Get([FromQuery] string guid)
        {
            var user = await _userService.GetUser(guid);
            return user != null ? Ok(user) : NotFound();
        }

        // POST: api/User
        [HttpPost]
        [Route("add")]
        [ProducesDefaultResponseType(typeof(IActionResult))]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var result = await _userManager.CreateAsync(user, "12345678");
            return result.Succeeded ? Ok() : BadRequest();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
