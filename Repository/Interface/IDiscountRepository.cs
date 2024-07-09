using SummerPracticePaul.Models;

namespace SummerPracticePaul.Repository.Interface
{
    public interface IDiscountRepository
    {
        Task<List<Discount>> GetAllDiscountsAsync();
        Task<Discount> GetDiscountByIdAsync(int id);
        Task<Discount> CreateDiscountAsync(Discount discount);
        Task<Discount> UpdateDiscountAsync(Discount discount);
        Task<bool> DeleteDiscountAsync(int id);
    }
}
