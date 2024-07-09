using System.ComponentModel.DataAnnotations;

namespace SummerPracticePaul.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public ICollection<GameCategory>? Categories { get; set; }
    }
}

