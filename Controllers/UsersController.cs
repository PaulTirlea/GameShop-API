using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Services;

namespace SummerPracticePaul.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UsersController(IUserService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet("api/users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _usersService.GetUsersDtoAsync();
            return users.ToList();
        }

        /// <summary>
        /// Get a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet("api/user/{id}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _usersService.GetUserDtoByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>No content if successful, not found if the user doesn't exist.</returns>
        [HttpPut("api/user/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutUser(UserDto user)
        {

            try
            {
                await _usersService.UpdateUserDtoAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_usersService.UserExists(user.Id))
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
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user object to be created.</param>
        /// <returns>The created user with its ID.</returns>

        [HttpPost("api/user")]
        [ProducesResponseType(typeof(User), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            await _usersService.CreateUserDtoAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
        }

        /// <summary>
        /// Delete a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>No content if successful, not found if the user doesn't exist.</returns>
        [HttpDelete("api/user/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _usersService.DeleteUserDtoAsync(id);
            return NoContent();
        }


    }
}
