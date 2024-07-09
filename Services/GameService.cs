using AutoMapper;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Repository.Interface;

namespace SummerPracticePaul.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameCategoryRepository _gameCategoryRepository;
        private readonly IDiscountService _discountService;
        private readonly IGameCategoryService _gameCategoryService;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IGameCategoryRepository gameCategoryRepository, IMapper mapper, IDiscountService discountService, IGameCategoryService gameCategoryService)
        {
            _gameRepository = gameRepository;
            _gameCategoryRepository = gameCategoryRepository;
            _mapper = mapper;
            _discountService = discountService;
            _gameCategoryService = gameCategoryService;
        }

        public async Task<List<GameDto>> GetAllGameDtosAsync()
        {
            var games = await _gameRepository.GetAllGamesAsync();
            var gameDtos = _mapper.Map<List<GameDto>>(games);

            foreach (var gameDto in gameDtos)
            {
                if (gameDto.DiscountId.HasValue)
                {
                    var discount = await _discountService.GetDiscountDtoByIdAsync(gameDto.DiscountId.Value);
                    if (discount != null)
                    {
                        gameDto.PriceAfterDiscount = gameDto.Price * (1 - discount.Value);
                    }
                }
                else
                {
                    gameDto.PriceAfterDiscount = gameDto.Price;
                }
            }

            return gameDtos;
        }

        public async Task<GameDto> GetGameDtoByIdAsync(int id)
        {
            var game = await _gameRepository.GetGameByIdAsync(id);
            var gameDto = _mapper.Map<GameDto>(game);

            if (gameDto.DiscountId.HasValue)
            {
                var discount = await _discountService.GetDiscountDtoByIdAsync(gameDto.DiscountId.Value);
                if (discount != null)
                {
                    gameDto.PriceAfterDiscount = gameDto.Price * (1 - discount.Value);
                }
            }
            else
            {
                gameDto.PriceAfterDiscount = gameDto.Price;
            }

            gameDto.Categories = _mapper.Map<ICollection<GameCategoryDto>>(game.Categories);

            return gameDto;
        }

        public async Task CreateGameDtoAsync(GameDto gameDto)
        {
            /*
            if (gameDto.Categories != null && gameDto.Categories.Any())
            {
                var firstCategoryDto = gameDto.Categories.First();

                var existingCategory = await _gameCategoryRepository.GetGameCategoryDtoByNameAsync(firstCategoryDto.Name);

                if (existingCategory != null)
                {
                    var game = _mapper.Map<Game>(gameDto);
                    game.Categories = new List<GameCategory> { _mapper.Map<GameCategory>(existingCategory) };
                    await _gameRepository.CreateGameAsync(game);
                }
                else
                {
                    var newCategory = new GameCategory { Name = firstCategoryDto.Name };
                    var game = _mapper.Map<Game>(gameDto);
                    game.Categories = new List<GameCategory> { newCategory };

                    await _gameCategoryRepository.CreateGameCategoryDtoAsync(newCategory);
                    await _gameRepository.CreateGameAsync(game);
                }
            }
            else
            {

            }
            */
            var game = _mapper.Map<Game>(gameDto);
            await _gameRepository.CreateGameAsync(game);
        }



        public async Task UpdateGameDtoAsync(GameDto gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);

            if (gameDto.DiscountId.HasValue)
            {
                var discount = await _discountService.GetDiscountDtoByIdAsync(gameDto.DiscountId.Value);
                if (discount != null)
                {
                    gameDto.PriceAfterDiscount = gameDto.Price * (1 - discount.Value);
                }
            }
            else
            {
                gameDto.PriceAfterDiscount = gameDto.Price;
            }

            await _gameRepository.UpdateGameAsync(game);

        }

        public async Task DeleteGameDtoAsync(int id)
        {
            await _gameRepository.DeleteGameAsync(id);
        }
    }
}
