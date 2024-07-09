using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Data;
using SummerPracticePaul.Repository.Interface;

namespace SummerPracticePaul.Repository
{
    public class InMemoryDiscountRepository : IDiscountRepository
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public InMemoryDiscountRepository(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Discount>> GetAllDiscountsAsync()
        {
            var discounts = await _context.Discounts.ToListAsync();
            return discounts;
        }

        public async Task<Discount> GetDiscountByIdAsync(int id)
        {
            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == id);
            return discount;
        }

        public async Task<Discount> CreateDiscountAsync(Discount discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task<Discount> UpdateDiscountAsync(Discount discount)
        {
            _context.Entry(discount).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task<bool> DeleteDiscountAsync(int id)
        {
            var discountToDelete = await _context.Discounts.FindAsync(id);
            if (discountToDelete == null)
                return false;

            _context.Discounts.Remove(discountToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
