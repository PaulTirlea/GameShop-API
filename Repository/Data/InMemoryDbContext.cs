using Microsoft.EntityFrameworkCore;
using SummerPracticePaul.Models;
namespace SummerPracticePaul.Repository.Data
{
    public class InMemoryDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            }); 
            */

            base.OnModelCreating(modelBuilder);
        }
    }
}
