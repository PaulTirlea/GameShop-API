using Microsoft.EntityFrameworkCore;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Data;
using SummerPracticePaul.Repository.Interface;

namespace SummerPracticePaul.Repository
{
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly InMemoryDbContext _context;

        public InMemoryGameRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            var games = await _context.Games.Include(g => g.Categories).ToListAsync();
            return games;
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            var game = await _context.Games.Include(g => g.Categories).FirstOrDefaultAsync(g => g.Id == id);
            return game;
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<Game> UpdateGameAsync(Game game)
        {
            var existingGame = await _context.Games.Include(g => g.Categories).FirstOrDefaultAsync(g => g.Id == game.Id);
            if (existingGame == null)
                return null;

            _context.Entry(existingGame).CurrentValues.SetValues(game);

            if (game.Categories != null)
            {
                foreach (var existingCategory in existingGame.Categories.ToList())
                {
                    _context.Entry(existingCategory).State = EntityState.Detached;
                }

                foreach (var category in game.Categories)
                {
                    if (category.Id == 0)
                    {
                        _context.Entry(category).State = EntityState.Added;
                    }
                    else
                    {
                        var existingCategory = await _context.GameCategories.FindAsync(category.Id);
                        if (existingCategory != null)
                        {
                            _context.Entry(existingCategory).State = EntityState.Unchanged;

                            if (existingGame.Categories != null && !existingGame.Categories.Any(c => c.Id == existingCategory.Id))
                            {
                                existingGame.Categories.Add(existingCategory);
                            }
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
            return existingGame;
        }


        public async Task<bool> DeleteGameAsync(int id)
        {
            var gameToDelete = await _context.Games.FindAsync(id);
            if (gameToDelete == null)
                return false;

            _context.Games.Remove(gameToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
