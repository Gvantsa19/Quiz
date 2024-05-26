//using FlatRockProject.Application.Models;
//using FlatRockProject.Application.Services.Roles;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace FlatRockProject.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RoleController : ControllerBase
//    {
//        private readonly IRoleService _roleService;

//        public RoleController(IRoleService roleService)
//        {
//            _roleService = roleService;
//        }

//        [Authorize(Roles = "Admin")]
//        [HttpGet("List")]
//        public async Task<IActionResult> GetList()
//        {
//            return Ok(await _roleService.GetRolesAsync());
//        }
//        [Authorize(Roles = "Admin")]
//        [HttpGet("UserRoles")]
//        public async Task<IActionResult> GetUserRoles(string email)
//        {
//            return Ok(await _roleService.GetUserRoleAsync(email));
//        }
//        [Authorize(Roles = "Admin")]
//        [HttpPost("CreateRoles")]
//        public async Task<IActionResult> PostRole(string[] roles)
//        {
//            var userRoles = await _roleService.AddRolesAsync(roles);

//            if (userRoles.Count == 0)
//            {
//                return BadRequest();
//            }

//            return Ok(userRoles);
//        } 
//        //[Authorize(Roles = "Admin")]
//        //[HttpPost("AddRoleToUser")]
//        //public async Task<IActionResult> AddUserRoles([FromBody] UserDto roles)
//        //{
//        //    return Ok(await _roleService.AddUserRoleAsync(roles.UserEmail, roles.Roles));
//        //}
//    }
//}
