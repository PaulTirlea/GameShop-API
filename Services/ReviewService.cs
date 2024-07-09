using AutoMapper;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Repository.Interface;

namespace SummerPracticePaul.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<List<ReviewDto>> GetReviewsForGameDtosAsync(int gameId)
        {
            var reviews = await _reviewRepository.GetReviewsByGameIdAsync(gameId);
            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);
            return reviewDtos;
        }

        public async Task<ReviewDto> CreateReviewDtoAsync(ReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            var createdReview = await _reviewRepository.AddReviewAsync(review);
            var createdReviewDto = _mapper.Map<ReviewDto>(createdReview);
            return createdReviewDto;
        }

        public async Task<ReviewDto> UpdateReviewDtoAsync(ReviewDto reviewDto)
        {
            var existingReview = await _reviewRepository.GetReviewByIdAsync(reviewDto.Id);
            if (existingReview == null)
                return null;

            _mapper.Map(reviewDto, existingReview);

            var result = await _reviewRepository.UpdateReviewAsync(existingReview);
            var resultDto = _mapper.Map<ReviewDto>(result);
            return resultDto;
        }


        public async Task<bool> DeleteReviewDtoAsync(int reviewId)
        {
            return await _reviewRepository.DeleteReviewAsync(reviewId);
        }

        public async Task<List<ReviewDto>> GetUserReviewDtosAsync(int userId)
        {
            var reviews = await _reviewRepository.GetReviewsByUserIdAsync(userId);
            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);
            return reviewDtos;
        }

        public async Task<List<ReviewDto>> GetAllReviewDtosAsync()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);
            return reviewDtos;
        }

        public async Task<double> GetAverageRatingForGameAsync(int gameId)
        {
            return await _reviewRepository.GetAverageRatingByGameIdAsync(gameId);
        }
    }
}
