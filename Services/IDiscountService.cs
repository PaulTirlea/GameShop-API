using SummerPracticePaul.Repository.Dto;

namespace SummerPracticePaul.Services
{
    public interface IDiscountService
    {
        Task<List<DiscountDto>> GetAllDiscountsDtosAsync();
        Task<DiscountDto> GetDiscountDtoByIdAsync(int id);
        Task CreateDiscountDtoAsync(DiscountDto discountDto);
        Task UpdateDiscountDtoAsync(DiscountDto discountDto);
        Task DeleteDiscountDtoAsync(int id);
    }
}
