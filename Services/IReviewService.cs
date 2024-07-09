using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetReviewsForGameDtosAsync(int gameId);
        Task<ReviewDto> CreateReviewDtoAsync(ReviewDto reviewDto);
        Task<ReviewDto> UpdateReviewDtoAsync(ReviewDto reviewDto);
        Task<bool> DeleteReviewDtoAsync(int reviewId);
        Task<List<ReviewDto>> GetUserReviewDtosAsync(int userId);
        Task<List<ReviewDto>> GetAllReviewDtosAsync();
        Task<double> GetAverageRatingForGameAsync(int gameId);
    }
}
