using System.ComponentModel.DataAnnotations;

namespace SummerPracticePaul.Repository.Dto
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Value { get; set; }
    }
}
