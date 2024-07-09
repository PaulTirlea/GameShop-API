using Microsoft.AspNetCore.Identity;
using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services
{
    public interface IControllPanelService
    {
        Task<IdentityResult> UpdateUserRolesAsync(UserDto userDto, IEnumerable<string> newRoles);
    }
}
