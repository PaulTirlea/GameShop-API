using Microsoft.EntityFrameworkCore;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Data;
using SummerPracticePaul.Repository.Interface;

namespace SummerPracticePaul.Repository
{
    public class InMemoryReviewRepository : IReviewRepository
    {
        private readonly InMemoryDbContext _context;

        public InMemoryReviewRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetReviewsByGameIdAsync(int gameId)
        {
            var reviews = await _context.Reviews.Where(r => r.GameId == gameId).ToListAsync();
            return reviews;
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var reviewToDelete = await _context.Reviews.FindAsync(reviewId);
            if (reviewToDelete == null)
                return false;

            _context.Reviews.Remove(reviewToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
            return review;
        }

        public async Task<List<Review>> GetReviewsByUserIdAsync(int userId)
        {
            var reviews = await _context.Reviews.Where(r => r.UserId == userId).ToListAsync();
            return reviews;
        }

        public async Task<double> GetAverageRatingByGameIdAsync(int gameId)
        {
            var averageRating = await _context.Reviews.Where(r => r.GameId == gameId).AverageAsync(r => r.Rating);
            return averageRating;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return reviews;
        }
    }
}
