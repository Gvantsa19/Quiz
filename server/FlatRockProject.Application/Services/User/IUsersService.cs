using FlatRockProject.Application.Models;
using Optional;

namespace FlatRockProject.Application.Services.User
{
    public interface IUsersService
    {
        Task<JwtDto> Login(LoginUserDto model);

        Task<ApplicationResult> Register(RegisterDto model);
    }
}
