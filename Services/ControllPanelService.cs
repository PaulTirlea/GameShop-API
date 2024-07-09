using Microsoft.AspNetCore.Identity;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services
{
    public class ControllPanelService : IControllPanelService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public ControllPanelService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> UpdateUserRolesAsync(UserDto userDto, IEnumerable<string> newRoles)
        {
            var existingUser = await _userManager.FindByIdAsync(userDto.Id.ToString());

            if (existingUser == null)
            {
                throw new ArgumentException("User not found.");
            }

            if (newRoles == null || !newRoles.Any())
            {
                throw new ArgumentException("User must have at least one role.");
            }

            if (string.IsNullOrEmpty(existingUser.SecurityStamp))
            {
                await _userManager.UpdateSecurityStampAsync(existingUser);
            }

            var existingRoles = await _userManager.GetRolesAsync(existingUser);
            var rolesToRemove = existingRoles.Except(newRoles);
            var rolesToAdd = newRoles.Except(existingRoles);

            await _userManager.RemoveFromRolesAsync(existingUser, rolesToRemove);
            return await _userManager.AddToRolesAsync(existingUser, rolesToAdd);
        }



    }
}
