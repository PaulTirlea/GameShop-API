using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services
{
    public interface IGameCategoryService
    {
        Task<List<GameCategoryDto>> GetAllGameCategoriesDtosAsync();
        Task<GameCategoryDto> GetGameCategoryDtoByIdAsync(int id);
        Task CreateGameCategoryDtoAsync(GameCategoryDto gameCategoryDto);
        Task UpdateGameCategoryDtoAsync(GameCategoryDto gameCategoryDto);
        Task DeleteGameCategoryDtoAsync(int id);
    }
}
