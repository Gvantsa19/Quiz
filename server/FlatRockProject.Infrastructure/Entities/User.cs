using FlatRockProject.Infrastructure.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace FlatRockProject.Infrastructure.Entities
{
    public class User: IdentityUser<string>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
    }
}
