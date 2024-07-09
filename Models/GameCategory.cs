using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SummerPracticePaul.Models
{
    public class GameCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Game>? Games { get; set; }
    }
}
