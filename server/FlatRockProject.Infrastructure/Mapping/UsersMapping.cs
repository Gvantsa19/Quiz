using AutoMapper;
using FlatRockProject.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace FlatRockProject.Infrastructure.Mapping
{
    public class UsersMapping : Profile
    {
        public UsersMapping()
        {
            CreateMap<User, UserDto>(MemberList.Destination);

            CreateMap<RegisterDto, User>(MemberList.Source)
                .ForMember(d => d.UserName, opts => opts.MapFrom(s => s.Email))
                .ForMember(s => s.PasswordHash, opts => opts.Ignore());
        }
    }
    public class UserDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
    public class RegisterDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
