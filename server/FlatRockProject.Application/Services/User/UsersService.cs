using AutoMapper;
using FlatRockProject.Application.Models;
using FlatRockProject.Infrastructure.Config;
using Microsoft.AspNetCore.Identity;
using Optional;
using System.Security.Claims;

namespace FlatRockProject.Application.Services.User
{
    public class UsersService : IUsersService
    {
        protected readonly UserManager<Infrastructure.Entities.User> _userManager;
        private readonly IJwtFactory _jwtFactory;

        public UsersService(UserManager<Infrastructure.Entities.User> userManager,
            IJwtFactory jwtFactory)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
        }

        public async Task<JwtDto> Login(LoginUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Invalid credentials.");
            }

            var jwt = new JwtDto
            {
                TokenString = _jwtFactory.GenerateEncodedToken(user.Id, user.Email, new List<Claim>())
            };

            return jwt;
        }
        public async Task<ApplicationResult> Register(RegisterDto model)
        {
            var user = new Infrastructure.Entities.User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                NormalizedUserName = model.Email,
                Email = model.Email,
                PasswordHash = model.Password,
                RegistrationDate = DateTime.Now
            };

            var result = (await _userManager.CreateAsync(user, model.Password));
            if (result.Succeeded)
            {
                return new ApplicationResult
                {
                    Data = result,
                    Success = true
                };
            }
            else
            {
                var errorMessages = result.Errors.Select(error => error.Description);
                var error = new Error(errorMessages);

                return new ApplicationResult
                {
                    Success = false,
                    Errors = new List<Error> { error }
                };
            }
        }

        public async Task<ApplicationResult> UpdateUser(string userId, UpdateUserDto model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ApplicationResult
                {
                    Success = false,
                    Errors = new List<Error> { new Error("User not found.") }
                };
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApplicationResult
                {
                    Success = true
                };
            }
            else
            {
                var errorMessages = result.Errors.Select(error => error.Description);
                var error = new Error(errorMessages);

                return new ApplicationResult
                {
                    Success = false,
                    Errors = new List<Error> { error }
                };
            }
        }
    }
}
