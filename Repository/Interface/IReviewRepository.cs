using SummerPracticePaul.Models;

namespace SummerPracticePaul.Repository.Interface
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviewsByGameIdAsync(int gameId);
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(int reviewId);
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<List<Review>> GetReviewsByUserIdAsync(int userId);
        Task<double> GetAverageRatingByGameIdAsync(int gameId);
        Task<List<Review>> GetAllReviewsAsync();
    }
}
