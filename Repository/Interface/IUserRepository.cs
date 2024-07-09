using SummerPracticePaul.Models;

namespace SummerPracticePaul.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        bool UserExists(int roleId);
    }
}
