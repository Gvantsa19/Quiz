using FlatRockProject.Infrastructure.Entities;

namespace FlatRockProject.Infrastructure.Repository
{
    public interface IUserRepository
    {
        User CreateUser(User user);
    }
}
