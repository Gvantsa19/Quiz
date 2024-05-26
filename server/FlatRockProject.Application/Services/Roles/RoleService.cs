//using FlatRockProject.Application.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace FlatRockProject.Application.Services.Roles
//{
//    public class RoleService : IRoleService
//    {
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly UserManager<IdentityUser> _userManager;

//        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
//        {
//            _roleManager = roleManager;
//            _userManager = userManager;
//        }
//        public async Task<List<string>> AddRolesAsync(string[] roles)
//        {
//            var rolesList = new List<string>();
//            foreach (var item in roles)
//            {
//                if (!await _roleManager.RoleExistsAsync(item))
//                {
//                    await _roleManager.CreateAsync(new IdentityRole(item));
//                    rolesList.Add(item);
//                };
//            }
//            return rolesList;
//        }
//        public async Task<bool> AddUserRoleAsync(string userEmail, string[] roles)
//        {
//            var user = await _userManager.FindByEmailAsync(userEmail);
//            var roleList = await ExistsRolesAsync(roles);

//            if (user != null && roleList.Count == roles.Length)
//            {
//                var assignRoles = await _userManager.AddToRolesAsync(user, roleList);

//                return assignRoles.Succeeded;
//            }
//            return false;
//        }
//        private async Task<List<string>> ExistsRolesAsync(string[] roles)
//        {
//            var rolesList = new List<string>();
//            foreach(var item in roles)
//            {
//                var role = await _roleManager.RoleExistsAsync(item);
//                if (role)
//                {
//                    rolesList.Add(item);
//                }
//            }
//            return rolesList;
//        }
//        public async Task<List<RoleModel>> GetRolesAsync()
//        {
//            return await _roleManager.Roles.Select(x =>
//            new RoleModel
//            {
//                Id = Guid.Parse(x.Id),
//                Name = x.Name
//            }
//            ).ToListAsync();
//        }
//        public async Task<List<string>> GetUserRoleAsync(string emailId)
//        {
//            var user = await _userManager.FindByEmailAsync(emailId);

//            var userRoles = await _userManager.GetRolesAsync(user);

//            return userRoles.ToList();
//        }
//    }
//}
