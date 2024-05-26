using FlatRockProject.Infrastructure.Entities;
using FlatRockProject.Infrastructure.Persistence;

namespace FlatRockProject.Infrastructure.Repository
{
    public class UserRepository 
    {
        private readonly FRPDbContext _context;

        //public UserRepository(FRPDbContext context)
        //{
        //    _context = context;
        //}
        //public User CreateUser(User user)
        //{
        //    _context.Users.Add(user);
        //   user.Id = _context.SaveChanges();

        //    return user;
        //}
    }
}
