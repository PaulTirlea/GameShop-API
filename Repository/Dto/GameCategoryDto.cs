using System.Text.Json.Serialization;

namespace SummerPracticePaul.Repository.Dto
{
    public class GameCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<GameDto>? Games { get; set; }
    }
}
