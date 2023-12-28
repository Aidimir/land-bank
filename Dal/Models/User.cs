using System;
using Microsoft.AspNetCore.Identity;

namespace Dal.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
    }
}