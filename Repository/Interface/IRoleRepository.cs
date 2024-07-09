using SummerPracticePaul.Models;

namespace SummerPracticePaul.Repository.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role> GetRoleAsync(int id);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int id);
        bool RoleExists(int roleId);
    }
}
