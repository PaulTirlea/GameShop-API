using AutoMapper;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository;
using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services
{
    public class GameCategoryService : IGameCategoryService
    {
        private readonly IGameCategoryRepository _gameCategoryRepository;
        private readonly IMapper _mapper;

        public GameCategoryService(IGameCategoryRepository gameCategoryRepository, IMapper mapper)
        {
            _gameCategoryRepository = gameCategoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GameCategoryDto>> GetAllGameCategoriesDtosAsync()
        {
            var gameCategories = await _gameCategoryRepository.GetAllGameCategoriesAsync();
            var gameCategoriesDtos = _mapper.Map<List<GameCategoryDto>>(gameCategories);
            return gameCategoriesDtos;
        }

        public async Task<GameCategoryDto> GetGameCategoryDtoByIdAsync(int id)
        {
            var gameCategory = await _gameCategoryRepository.GetGameCategoryByIdAsync(id);
            var gameCategoryDto = _mapper.Map<GameCategoryDto>(gameCategory);
            return gameCategoryDto;
        }

        public async Task CreateGameCategoryDtoAsync(GameCategoryDto gameCategoryDto)
        {
            var gameCategory = _mapper.Map<GameCategory>(gameCategoryDto);
            await _gameCategoryRepository.CreateGameCategoryAsync(gameCategory);
        }

        public async Task UpdateGameCategoryDtoAsync(GameCategoryDto gameCategoryDto)
        {
            var gameCategory = _mapper.Map<GameCategory>(gameCategoryDto);
            await _gameCategoryRepository.UpdateGameCategoryAsync(gameCategory);
        }

        public async Task DeleteGameCategoryDtoAsync(int id)
        {
            await _gameCategoryRepository.DeleteGameCategoryAsync(id);
        }
    }
}
