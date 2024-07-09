using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SummerPracticePaul.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class GamesController : BaseController
    {
        private readonly IGameService _gameService;
        private readonly IGameCategoryService _gameCategoryService;
        private readonly IDiscountService _discountService;

        public GamesController(IGameService gameService, IGameCategoryService gameCategoryService, IDiscountService discountService)
        {
            _gameService = gameService;
            _gameCategoryService = gameCategoryService;
            _discountService = discountService;
        }

        /// <summary>
        /// Get a list of all games.
        /// </summary>
        /// <returns>A list of games.</returns>
        [AllowAnonymous]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames()
        {
            var games = await _gameService.GetAllGameDtosAsync();
            return Ok(games);
        }

        /// <summary>
        /// Get a game by its ID.
        /// </summary>
        /// <param name="id">The ID of the game.</param>
        /// <returns>The game with the specified ID.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [SwaggerOperation("GetGameById")]
        [ProducesResponseType(typeof(GameDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GameDto>> GetGameById(int id)
        {
            var game = await _gameService.GetGameDtoByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        /// <summary>
        /// Add a new game.
        /// </summary>
        /// <param name="game">The game to add.</param>
        /// <returns>The added game.</returns>
        [HttpPost()]
        [SwaggerOperation("CreateGame")]
        [ProducesResponseType(typeof(GameDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<GameDto>> AddGame(GameDto gameDto)
        {
            if (gameDto.DiscountId.HasValue)
            {
                var discount = await _discountService.GetDiscountDtoByIdAsync(gameDto.DiscountId.Value);
                if (discount != null)
                {
                    gameDto.PriceAfterDiscount = gameDto.Price * (1 - discount.Value);
                }
            }
            else
            {
                gameDto.PriceAfterDiscount = gameDto.Price;
            }

            await _gameService.CreateGameDtoAsync(gameDto);
            return CreatedAtAction(nameof(GetGameById), new { id = gameDto.Id }, gameDto);
        }

        /// <summary>
        /// Update an existing game.
        /// </summary>
        /// <param name="id">The ID of the game to update.</param>
        /// <param name="game">The updated game data.</param>
        /// <returns>The updated game.</returns>
        [HttpPut()]
        [SwaggerOperation("UpdateGame")]
        [ProducesResponseType(typeof(GameDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GameDto>> UpdateGame(GameDto gameDto)
        {
            if (gameDto.Categories != null)
            {
                foreach (var categoryDto in gameDto.Categories)
                {
                    var existingCategory = await _gameCategoryService.GetGameCategoryDtoByIdAsync(categoryDto.Id);
                    if (existingCategory == null)
                    {
                        return NotFound($"Category with ID {categoryDto.Id} does not exist.");
                    }
                }
            }


            await _gameService.UpdateGameDtoAsync(gameDto);

            return Ok(gameDto);
        }

        /// <summary>
        /// Delete a game by its ID.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteGame")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var existingGame = await _gameService.GetGameDtoByIdAsync(id);
            if (existingGame == null)
            {
                return NotFound();
            }

            await _gameService.DeleteGameDtoAsync(id);
            return NoContent();
        }
    }
}
