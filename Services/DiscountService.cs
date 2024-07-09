using AutoMapper;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Dto;
using SummerPracticePaul.Repository.Interface;

namespace SummerPracticePaul.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<List<DiscountDto>> GetAllDiscountsDtosAsync()
        {
            var discounts = await _discountRepository.GetAllDiscountsAsync();
            var discountDtos = _mapper.Map<List<DiscountDto>>(discounts);
            return discountDtos;
        }

        public async Task<DiscountDto> GetDiscountDtoByIdAsync(int id)
        {
            var discount = await _discountRepository.GetDiscountByIdAsync(id);
            var discountDto = _mapper.Map<DiscountDto>(discount);
            return discountDto;
        }

        public async Task CreateDiscountDtoAsync(DiscountDto discountDto)
        {
            var discount = _mapper.Map<Discount>(discountDto);
            await _discountRepository.CreateDiscountAsync(discount);
        }

        public async Task UpdateDiscountDtoAsync(DiscountDto discountDto)
        {
            var discount = _mapper.Map<Discount>(discountDto);
            await _discountRepository.UpdateDiscountAsync(discount);
        }

        public async Task DeleteDiscountDtoAsync(int id)
        {
            await _discountRepository.DeleteDiscountAsync(id);
        }
    }
}
