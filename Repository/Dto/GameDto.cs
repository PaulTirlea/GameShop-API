namespace SummerPracticePaul.Repository.Dto
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public ICollection<GameCategoryDto>? Categories { get; set; }
    }
}
