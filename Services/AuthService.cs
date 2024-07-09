using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SummerPracticePaul.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SymmetricSecurityKey _key;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, SymmetricSecurityKey key, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _key = key;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateUserAsync(UserDto registrationDto)
        {
            var user = _mapper.Map<User>(registrationDto);
            return await _userManager.CreateAsync(user, registrationDto.Password);
        }


        public async Task<SignInResult> SignInAsync(string username, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task<string> GenerateJwtTokenAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return _mapper.Map<UserDto>(user);
        }


        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _roleManager.FindByIdAsync(roleId.ToString());
        }






        public async Task<IdentityResult> AddToRoleAsync(UserDto userDto, string role)
        {
            var user = await _userManager.FindByNameAsync(userDto.UserName);

            if (user != null)
            {
                if (string.IsNullOrEmpty(user.SecurityStamp))
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }

                return await _userManager.AddToRoleAsync(user, role);
            }

            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }



    }
}
