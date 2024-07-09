using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Data;

namespace SummerPracticePaul.Repository
{
    public class InMemoryGameCategoryRepository : IGameCategoryRepository
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public InMemoryGameCategoryRepository(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GameCategory>> GetAllGameCategoriesAsync()
        {
            var gameCategories = await _context.GameCategories.ToListAsync();
            return gameCategories;
        }

        public async Task<GameCategory> GetGameCategoryByIdAsync(int id)
        {
            var gameCategory = await _context.GameCategories.FirstOrDefaultAsync(g => g.Id == id);
            return gameCategory;
        }

        public async Task<GameCategory> CreateGameCategoryAsync(GameCategory gameCategory)
        {
            _context.GameCategories.Add(gameCategory);
            await _context.SaveChangesAsync();
            return gameCategory;
        }

        public async Task<GameCategory> UpdateGameCategoryAsync(GameCategory gameCategory)
        {
            _context.Entry(gameCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return gameCategory;
        }

        public async Task<bool> DeleteGameCategoryAsync(int id)
        {
            var gameCategoryToDelete = await _context.GameCategories.FirstOrDefaultAsync(g => g.Id == id);
            if (gameCategoryToDelete == null)
                return false;

            _context.GameCategories.Remove(gameCategoryToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
