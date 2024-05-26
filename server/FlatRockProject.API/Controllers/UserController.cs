using FlatRockProject.Application.Models;
using FlatRockProject.Application.Services.User;
using FlatRockProject.Infrastructure.Entities;
using FlatRockProject.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlatRockProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpPost("login")]
        [ProducesResponseType(typeof(JwtDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            return Ok(await _usersService.Login(model));
        }
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            return Ok(await _usersService.Register(model));
        }
    }
}
