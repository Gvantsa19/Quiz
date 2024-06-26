﻿using System.ComponentModel.DataAnnotations;

namespace FlatRockProject.Application.Models
{
    public class LoginUserDto
    {
        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
