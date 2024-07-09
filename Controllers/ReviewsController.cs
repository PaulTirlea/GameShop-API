using Microsoft.AspNetCore.Mvc;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SummerPracticePaul.Controllers
{
    [ApiController]
    [Route("api/game/{gameId}/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Get reviews for a specific game.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("GetReviewsForGame")]
        [ProducesResponseType(typeof(List<ReviewDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<ReviewDto>>> GetReviewsForGame(int gameId)
        {
            var reviews = await _reviewService.GetReviewsForGameDtosAsync(gameId);
            return Ok(reviews);
        }

        /// <summary>
        /// Create a new review for a game.
        /// </summary>
        [HttpPost]
        [SwaggerOperation("CreateReview")]
        [ProducesResponseType(typeof(ReviewDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ReviewDto>> CreateReview(int gameId, ReviewDto reviewDto)
        {
            if (gameId != reviewDto.GameId)
            {
                return BadRequest("Game ID in URL does not match Game ID in review data.");
            }

            var createdReviewDto = await _reviewService.CreateReviewDtoAsync(reviewDto);
            return CreatedAtAction(nameof(GetReviewsForGame), new { gameId = gameId, reviewId = createdReviewDto.Id }, createdReviewDto);
        }

        /// <summary>
        /// Update an existing review.
        /// </summary>
        [HttpPut("{reviewId}")]
        [SwaggerOperation("UpdateReview")]
        [ProducesResponseType(typeof(ReviewDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ReviewDto>> UpdateReview(int gameId, int reviewId, ReviewDto reviewDto)
        {
            if (gameId != reviewDto.GameId)
            {
                return BadRequest("Game ID in URL does not match Game ID in review data.");
            }

            if (reviewId != reviewDto.Id)
            {
                return BadRequest("Review ID in URL does not match Review ID in review data.");
            }

            var updatedReviewDto = await _reviewService.UpdateReviewDtoAsync(reviewDto);
            if (updatedReviewDto == null)
            {
                return NotFound();
            }

            return Ok(updatedReviewDto);
        }

        /// <summary>
        /// Delete a review.
        /// </summary>
        [HttpDelete("{reviewId}")]
        [SwaggerOperation("DeleteReview")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReview(int gameId, int reviewId)
        {
            var isDeleted = await _reviewService.DeleteReviewDtoAsync(reviewId);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get the average rating for a game.
        /// </summary>
        [HttpGet("/api/games/{gameId}/average-rating")]
        [SwaggerOperation("GetAverageRatingForGame")]
        [ProducesResponseType(typeof(double), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<double>> GetAverageRatingForGame(int gameId)
        {
            var averageRating = await _reviewService.GetAverageRatingForGameAsync(gameId);
            if (averageRating == null)
            {
                return NotFound();
            }
            return Ok(averageRating);
        }

        /// <summary>
        /// Get reviews left by a user.
        /// </summary>
        [HttpGet("/api/users/{userId}/reviews")]
        [SwaggerOperation("GetUserReviews")]
        [ProducesResponseType(typeof(List<ReviewDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<ReviewDto>>> GetUserReviews(int userId)
        {
            var userReviews = await _reviewService.GetUserReviewDtosAsync(userId);
            if (userReviews == null)
            {
                return NotFound();
            }
            return Ok(userReviews);
        }

        /// <summary>
        /// Get all reviews.
        /// </summary>
        [HttpGet("/api/reviews")]
        [SwaggerOperation("GetAllReviews")]
        [ProducesResponseType(typeof(List<ReviewDto>), 200)]
        public async Task<ActionResult<List<ReviewDto>>> GetAllReviews()
        {
            var allReviews = await _reviewService.GetAllReviewDtosAsync();
            return Ok(allReviews);
        }
    }
}
