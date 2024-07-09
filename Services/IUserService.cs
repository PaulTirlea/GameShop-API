using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersDtoAsync();
        Task<UserDto> GetUserDtoByIdAsync(int id);
        Task CreateUserDtoAsync(UserDto userDto);
        Task UpdateUserDtoAsync(UserDto userDto);
        Task<bool> DeleteUserDtoAsync(int id);
        bool UserExists(int roleId);
    }
}
