using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Services;

namespace SummerPracticePaul.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, UserManager<User> userManager, IMapper mapper)
        {
            _authService = authService;
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="registrationDto">The registration details.</param>
        /// <returns>OK if successful, BadRequest if there's an error.</returns>
        [HttpPost("api/auth/register")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register(UserDto registrationDto)
        {
            var result = await _authService.CreateUserAsync(registrationDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _authService.AddToRoleAsync(registrationDto, "CLIENT");

            var user = await _authService.GetUserByUsernameAsync(registrationDto.UserName);
            var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(user));

            return Ok(new
            {
                Message = "Registration successful",
                Roles = roles
            });
        }



        /// <summary>
        /// Log in a user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>OK if successful with authentication token, BadRequest if there's an error.</returns>
        [HttpPost("api/auth/login")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _authService.SignInAsync(username, password, rememberMe: false);
            if (result.Succeeded)
            {
                var userDto = await _authService.GetUserByUsernameAsync(username);
                var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(userDto));

                var token = await _authService.GenerateJwtTokenAsync(userDto);

                return Ok(new
                {
                    Token = token,
                    Roles = roles,
                    Message = "Login successful"
                });
            }
            return BadRequest("Invalid login attempt");
        }
    }
}
