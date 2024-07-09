using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SummerPracticePaul.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class GameCategoriesController : ControllerBase
    {
        private readonly IGameCategoryService _gameCategoryService;

        public GameCategoriesController(IGameCategoryService gameCategoryService)
        {
            _gameCategoryService = gameCategoryService;
        }

        /// <summary>
        /// Get a list of all game categories.
        /// </summary>
        /// <returns>A list of game categories.</returns>
        [HttpGet("api/game-categories")]
        [ProducesResponseType(typeof(IEnumerable<GameCategoryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameCategoryDto>>> GetGameCategories()
        {
            var categories = await _gameCategoryService.GetAllGameCategoriesDtosAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get a game category by its ID.
        /// </summary>
        /// <param name="id">The ID of the game category.</param>
        /// <returns>The game category with the specified ID.</returns>
        [HttpGet("api/game-category/{id}")]
        [SwaggerOperation("GetGameCategoryById")]
        [ProducesResponseType(typeof(GameCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameCategoryDto>> GetGameCategory(int id)
        {
            var gameCategory = await _gameCategoryService.GetGameCategoryDtoByIdAsync(id);
            if (gameCategory == null)
            {
                return NotFound();
            }
            return Ok(gameCategory);
        }

        /// <summary>
        /// Create a new game category.
        /// </summary>
        /// <param name="gameCategory">The game category to create.</param>
        /// <returns>The created game category.</returns>
        [HttpPost("api/game-category")]
        [SwaggerOperation("CreateGameCategory")]
        [ProducesResponseType(typeof(GameCategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GameCategoryDto>> CreateGameCategory(GameCategoryDto gameCategoryDto)
        {
            await _gameCategoryService.CreateGameCategoryDtoAsync(gameCategoryDto);
            return CreatedAtAction(nameof(GetGameCategory), new { id = gameCategoryDto.Id }, gameCategoryDto);
        }

        /// <summary>
        /// Update an existing game category.
        /// </summary>
        /// <param name="gameCategory">The updated game category data.</param>
        /// <returns>The updated game category.</returns>
        [HttpPut("api/game-category")]
        [SwaggerOperation("UpdateGameCategory")]
        [ProducesResponseType(typeof(GameCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameCategoryDto>> UpdateGameCategory(GameCategoryDto gameCategoryDto)
        {

            await _gameCategoryService.UpdateGameCategoryDtoAsync(gameCategoryDto);
            return Ok(gameCategoryDto);
        }

        /// <summary>
        /// Delete a game category by its ID.
        /// </summary>
        /// <param name="id">The ID of the game category to delete.</param>
        [HttpDelete("api/game-category/{id}")]
        [SwaggerOperation("DeleteGameCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGameCategory(int id)
        {
            var existingGameCategory = await _gameCategoryService.GetGameCategoryDtoByIdAsync(id);
            if (existingGameCategory == null)
            {
                return NotFound();
            }

            await _gameCategoryService.DeleteGameCategoryDtoAsync(id);
            return NoContent();
        }
    }
}
