using System;
using Microsoft.AspNetCore.Identity;

namespace Api.Controllers.Authorization.DTO.ResponseModels
{
    public class AuthResponse
    {
        public string Email { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Token { get; set; } = null!;

        public List<IdentityRole> Roles { get; set; } = null!;
    }
}