using SummerPracticePaul.Models;

namespace SummerPracticePaul.Repository.Interface
{
    public interface IGameRepository
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task<Game> CreateGameAsync(Game game);
        Task<Game> UpdateGameAsync(Game game);
        Task<bool> DeleteGameAsync(int id);
    }
}
