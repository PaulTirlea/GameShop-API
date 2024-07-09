using AutoMapper;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Repository.Interface;
using SummerPracticePaul.Services.Interface;

namespace SummerPracticePaul.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetRolesDtosAsync()
        {
            var roles = await _roleRepository.GetRolesAsync();
            var rolesDto = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return rolesDto;
        }

        public async Task<RoleDto> GetRoleDtoAsync(int id)
        {
            var role = await _roleRepository.GetRoleAsync(id);
            var roleDto = _mapper.Map<RoleDto>(role);
            return roleDto;
        }

        public async Task CreateRoleDtoAsync(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _roleRepository.CreateRoleAsync(role);
        }

        public async Task UpdateRoleDtoAsync(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _roleRepository.UpdateRoleAsync(role);
        }

        public async Task DeleteRoleDtoAsync(int id)
        {
            await _roleRepository.DeleteRoleAsync(id);
        }

        public bool RoleExists(int roleId)
        {
            return _roleRepository.RoleExists(roleId);
        }
    }
}
