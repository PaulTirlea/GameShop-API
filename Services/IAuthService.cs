using Microsoft.AspNetCore.Identity;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> CreateUserAsync(UserDto userRegistration);
        Task<SignInResult> SignInAsync(string username, string password, bool rememberMe);
        Task<string> GenerateJwtTokenAsync(UserDto userDto);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<IdentityResult> AddToRoleAsync(UserDto registrationDto, string role);
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<UserDto> GetUserByIdAsync(int userId);
    }
}
