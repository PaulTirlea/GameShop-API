using Microsoft.AspNetCore.Identity;
using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Repository
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateUserAsync(UserDto userRegistration);
        Task<SignInResult> SignInAsync(string username, string password, bool rememberMe);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<IdentityResult> AddToRoleAsync(UserDto userDto, string role);
    }
}
