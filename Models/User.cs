using Microsoft.AspNetCore.Identity;

namespace SummerPracticePaul.Models
{
    public class User : IdentityUser<int>
    {
        //[JsonIgnore]
        //public ICollection<UserRole> UserRoles { get; set; }
    }
}
