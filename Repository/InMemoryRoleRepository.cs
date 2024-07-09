using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Data;
using SummerPracticePaul.Repository.Interface;

namespace SummerPracticePaul.Repository
{
    public class InMemoryRoleRepository : IRoleRepository
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public InMemoryRoleRepository(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            return role;
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {

            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task DeleteRoleAsync(int id)
        {
            var roleToDelete = await _context.Roles.FindAsync(id);
            if (roleToDelete != null)
            {
                _context.Roles.Remove(roleToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public bool RoleExists(int roleId)
        {
            return _context.Roles.Any(r => r.Id == roleId);
        }
    }
}
