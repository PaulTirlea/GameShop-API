using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SummerPracticePaul.Models;
using SummerPracticePaul.Services;

namespace SummerPracticePaul.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class ControllPanelController : ControllerBase
    {
        private readonly ControllPanelService _controllPanelService;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly IGameService _gameService;
        private readonly IDiscountService _discountService;

        public ControllPanelController(ControllPanelService controllPanelService, IAuthService authService,
            UserManager<User> userManager, IGameService gameService, IDiscountService discountService)
        {
            _controllPanelService = controllPanelService;
            _authService = authService;
            _userManager = userManager;
            _gameService = gameService;
            _discountService = discountService;
        }

        /// <summary>
        /// Change roles for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="newRoles">A list of new roles to assign.</param>
        /// <returns>OK if successful, NotFound if user not found, BadRequest if there's an error.</returns>
        //[Authorize(Policy = "AdminOnly")]
        [HttpPut("api/user/{userId}/role")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ChangeUserRoles(int userId, IEnumerable<string> newRoles)
        {
            var user = await _authService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _controllPanelService.UpdateUserRolesAsync(user, newRoles);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User roles updated successfully" });
        }

        /// <summary>
        /// Assign a discount to a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="discountId">The ID of the discount to assign.</param>
        /// <returns>OK if successful, NotFound if game or discount not found, BadRequest if there's an error.</returns>
        [HttpPut("api/game/{gameId}/discount")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AssignDiscountToGame(int gameId, int discountId)
        {
            var game = await _gameService.GetGameDtoByIdAsync(gameId);
            if (game == null)
            {
                return NotFound("Game not found");
            }

            var discount = await _discountService.GetDiscountDtoByIdAsync(discountId);
            if (discount == null)
            {
                return NotFound("Discount not found");
            }

            game.DiscountId = discountId;
            await _gameService.UpdateGameDtoAsync(game);

            return Ok(new { Message = "Discount assigned to the game successfully" });
        }
    }
}

