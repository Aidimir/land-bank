using System;
using Logic.Interfaces;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.Authorization.DTO.RequestModels;
using Api.Controllers.Authorization.DTO.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Dal;
using Dal.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers.Authorization
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _context;

        private readonly UserManager<User> _userManager;

        private readonly IConfiguration _configuration;

        public AuthController(UserContext context,
            UserManager<User> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }


        [HttpPost("/register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (findUser == null)
            {
                String messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                           .Select(v => v.ErrorMessage + " " + v.Exception));
                throw new Exception($"Пользователь {request.Email} не найден. {messages}");
            }

            await _userManager.AddToRolesAsync(findUser, request.Roles);

            return await Authenticate(new AuthRequest
            {
                Email = request.Email,
                Password = request.Password
            });
        }


        [HttpPost("/login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var managedUser = await _userManager.FindByEmailAsync(request.Email);

            if (managedUser == null)
                return BadRequest("неправильный");

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPasswordValid)
                return BadRequest("Bad credentials");

            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user is null)
                return Unauthorized();

            var roleIds = await _context.UserRoles
                .Where(r => r.UserId == user.Id)
                .Select(x => x.RoleId)
                .ToListAsync();
            var roles = _context.Roles
                .Where(x => roleIds.Contains(x.Id))
                .ToList();

            var accessToken = await GenerateJwt(user);

            await _context.SaveChangesAsync();

            return Ok(new AuthResponse
            {
                Email = user.Email!,
                Token = accessToken,
                Roles = roles
            }) ;
        }

        private async Task<string> GenerateJwt(User user)
        {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var roles = await _userManager.GetRolesAsync(user);
            AddRolesToClaims(claims, roles);

            var jwt = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                notBefore: DateTime.Now,
        claims: claims,
        expires: DateTime.Now.Add(TimeSpan.FromDays(365)),
       signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!)),
       SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }
    }
}

