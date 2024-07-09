using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SummerPracticePaul.Models;

namespace SummerPracticePaul.Repository.Data
{
    public class UserDbContext : IdentityDbContext<User, Role, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
    }
}
