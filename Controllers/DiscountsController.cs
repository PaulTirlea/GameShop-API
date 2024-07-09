using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SummerPracticePaul.Controllers
{
    [ApiController]
    //[Authorize(Policy = "AdminOnly")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        /// <summary>
        /// Get a list of all game discounts.
        /// </summary>
        /// <returns>A list of game discounts.</returns>
        [HttpGet("api/discounts")]
        [SwaggerOperation("GetAllDiscounts")]
        [ProducesResponseType(typeof(IEnumerable<DiscountDto>), 200)]
        public async Task<ActionResult<IEnumerable<DiscountDto>>> GetAllDiscounts()
        {
            var discounts = await _discountService.GetAllDiscountsDtosAsync();
            return Ok(discounts);
        }

        /// <summary>
        /// Get a game discount by its ID.
        /// </summary>
        /// <param name="id">The ID of the game discount.</param>
        /// <returns>The game discount with the specified ID.</returns>
        [AllowAnonymous]
        [HttpGet("api/discount/{id}")]
        [SwaggerOperation("GetDiscountById")]
        [ProducesResponseType(typeof(DiscountDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DiscountDto>> GetDiscountById(int id)
        {
            var discount = await _discountService.GetDiscountDtoByIdAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            return Ok(discount);
        }

        /// <summary>
        /// Create a new game discount.
        /// </summary>
        /// <param name="discountDto">The game discount to create.</param>
        /// <returns>The created game discount.</returns>
        [HttpPost("api/discount")]
        [SwaggerOperation("CreateDiscount")]
        [ProducesResponseType(typeof(DiscountDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateDiscount(DiscountDto discountDto)
        {
            await _discountService.CreateDiscountDtoAsync(discountDto);
            return CreatedAtAction(nameof(GetDiscountById), new { id = discountDto.Id }, discountDto);
        }


        /// <summary>
        /// Update an existing game discount.
        /// </summary>
        /// <param name="discountDto">The updated game discount data.</param>
        /// <returns>The updated game discount.</returns>
        [HttpPut("api/discount")]
        [SwaggerOperation("UpdateDiscount")]
        [ProducesResponseType(typeof(DiscountDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateDiscount(DiscountDto discountDto)
        {
            await _discountService.UpdateDiscountDtoAsync(discountDto);
            return Ok(discountDto);
        }

        /// <summary>
        /// Delete a game discount by its ID.
        /// </summary>
        /// <param name="id">The ID of the game discount to delete.</param>
        [HttpDelete("api/discount/{id}")]
        [SwaggerOperation("DeleteDiscount")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            await _discountService.DeleteDiscountDtoAsync(id);
            return NoContent();
        }
    }
}
