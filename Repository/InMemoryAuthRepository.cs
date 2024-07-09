using Microsoft.AspNetCore.Identity;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Data;
using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Repository
{
    public class InMemoryAuthRepository : IAuthRepository
    {
        private readonly UserDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public InMemoryAuthRepository(UserDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserDto userDto)
        {
            var newUser = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };
            var result = await _userManager.CreateAsync(newUser, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(newUser);
            }
            return result;
        }

        public async Task<SignInResult> SignInAsync(string username, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<IdentityResult> AddToRoleAsync(UserDto userDto, string role)
        {
            var user = await _userManager.FindByNameAsync(userDto.UserName);
            return await _userManager.AddToRoleAsync(user, role);
        }

    }
}
