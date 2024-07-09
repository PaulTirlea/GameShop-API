using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetRolesDtosAsync();
        Task<RoleDto> GetRoleDtoAsync(int id);
        Task CreateRoleDtoAsync(RoleDto roleDto);
        Task UpdateRoleDtoAsync(RoleDto roleDto);
        Task DeleteRoleDtoAsync(int id);
        bool RoleExists(int roleId);

    }
}
