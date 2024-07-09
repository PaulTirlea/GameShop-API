using SummerPracticePaul.Models;

namespace SummerPracticePaul.Repository
{
    public interface IGameCategoryRepository
    {
        Task<List<GameCategory>> GetAllGameCategoriesAsync();
        Task<GameCategory> GetGameCategoryByIdAsync(int id);
        Task<GameCategory> CreateGameCategoryAsync(GameCategory gameCategory);
        Task<GameCategory> UpdateGameCategoryAsync(GameCategory gameCategory);
        Task<bool> DeleteGameCategoryAsync(int id);
    }
}
