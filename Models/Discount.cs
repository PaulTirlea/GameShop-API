using System.ComponentModel.DataAnnotations;

namespace SummerPracticePaul.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Value { get; set; }
        public virtual ICollection<Game>? Games { get; set; }
    }
}
