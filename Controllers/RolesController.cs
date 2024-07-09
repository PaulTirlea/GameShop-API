using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Services.Interface;

namespace SummerPracticePaul.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns>A list of all roles.</returns>
        [HttpGet("api/roles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            var roles = await _roleService.GetRolesDtosAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Get a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role.</param>
        /// <returns>The role with the specified ID.</returns>
        [HttpGet("api/role/{id}")]
        [ProducesResponseType(typeof(Role), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RoleDto>> GetRole(int id)
        {
            var role = await _roleService.GetRoleDtoAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        /// <summary>
        /// Update a role.
        /// </summary>
        /// <param name="id">The ID of the role.</param>
        /// <param name="role">The updated role object.</param>
        /// <returns>No content if successful, not found if the role doesn't exist.</returns>
        [HttpPut("api/role/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutRole(RoleDto roleDto)
        {

            try
            {
                await _roleService.UpdateRoleDtoAsync(roleDto);
            }
            catch (Exception)
            {
                if (!_roleService.RoleExists(roleDto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Create a new role.
        /// </summary>
        /// <param name="role">The role object to be created.</param>
        /// <returns>The created role with its ID.</returns>
        [HttpPost("api/role")]
        [ProducesResponseType(typeof(Role), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RoleDto>> PostRole(RoleDto roleDto)
        {
            await _roleService.CreateRoleDtoAsync(roleDto);
            return CreatedAtAction(nameof(GetRole), new { id = roleDto.Id }, roleDto);
        }

        /// <summary>
        /// Delete a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to be deleted.</param>
        /// <returns>No content if successful, not found if the role doesn't exist.</returns>
        [HttpDelete("api/role/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var existingRole = await _roleService.GetRoleDtoAsync(id);
            if (existingRole == null)
            {
                return NotFound();
            }
            await _roleService.DeleteRoleDtoAsync(id);
            return NoContent();
        }

    }
}
