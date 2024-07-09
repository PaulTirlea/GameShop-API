using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services
{
    public interface IGameService
    {
        Task<List<GameDto>> GetAllGameDtosAsync();
        Task<GameDto> GetGameDtoByIdAsync(int id);
        Task CreateGameDtoAsync(GameDto gameDto);
        Task UpdateGameDtoAsync(GameDto gameDto);
        Task DeleteGameDtoAsync(int id);
    }
}
