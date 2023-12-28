using Dal.Models;
using Microsoft.AspNetCore.Identity;

namespace Logic.Models
{
    public class LoginResult
    {
        public User User;

        public string JwtToken;

        public LoginResult(User user, string jwtToken)
        {
            User = user;
            JwtToken = jwtToken;
        }
    }
}