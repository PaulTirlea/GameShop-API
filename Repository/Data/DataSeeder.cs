using Microsoft.AspNetCore.Identity;
using SummerPracticePaul.Models;

namespace SummerPracticePaul.Repository.Data
{
    public class DataSeeder
    {
        public static void Initialize(InMemoryDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (context.Games.Any() && context.GameCategories.Any())
            {
                return;
            }

            var actionCategory = new GameCategory { Id = 1, Name = "Action" };
            var adventureCategory = new GameCategory { Id = 2, Name = "Adventure" };
            var puzzleCategory = new GameCategory { Id = 3, Name = "Puzzle" };

            var games = new Game[]
            {
        new Game
        {
            Id = 1,
            Title = "Assassin's Creed Valhalla",
            Description = "Explore the Viking age.",
            Price = 59.99m,
            DiscountId= 1,
            Categories = new List<GameCategory>
            {
                actionCategory,adventureCategory
            }
        },
        new Game
        {
            Id = 2,
            Title = "The Witcher 3: Wild Hunt",
            Description = "Embark on an epic journey as Geralt of Rivia.",
            Price = 39.99m,
            DiscountId= 2,
            Categories = new List<GameCategory>
            {
                actionCategory,adventureCategory
            }
        },
        new Game
        {
            Id = 3,
            Title = "Portal 2",
            Description = "Solve puzzles with your portal gun.",
            Price = 19.99m,
            Categories = new List<GameCategory>
            {
                puzzleCategory
            }
        },
            };

            context.GameCategories.AddRange(actionCategory, adventureCategory, puzzleCategory);
            context.Games.AddRange(games);

            var discounts = new Discount[]
           {
                new Discount
                {
                    Id = 1,
                    Name = "Summer Sale",
                    Description = "Get ready for summer with great discounts!",
                    Value = 0.2m,  // 20% discount
                },
                new Discount
                {
                    Id = 2,
                    Name = "Flash Sale",
                    Description = "Limited time offer on selected games.",
                    Value = 0.1m,  // 10% discount
                },
                new Discount
                {
                    Id = 3,
                    Name = "Holiday Special",
                    Description = "Special discounts for the holiday season.",
                    Value = 0.15m, // 15% discount
                },
           };

            context.Discounts.AddRange(discounts);
            context.SaveChanges();
            SeedUsersAndRoles(userManager, roleManager);

            var clientUser = userManager.FindByNameAsync("clientuser").Result;
            var adminUser = userManager.FindByNameAsync("adminuser").Result;

            var reviews = new Review[]
            {
                new Review
                {
                    Id = 1,
                    Comment = "Great game! I really enjoyed the storyline and gameplay.",
                    Rating = 5,
                    UserId = clientUser.Id,
                    GameId = 1
                },
                new Review
                {
                    Id = 2,
                    Comment = "Beautiful open world and exciting combat mechanics.",
                    Rating = 4,
                    UserId = clientUser.Id,
                    GameId = 2
                }
            };

            context.Reviews.AddRange(reviews);
            context.SaveChanges();
        }
        private static void SeedUsersAndRoles(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var hasher = new PasswordHasher<User>();

            var clientRole = new Role { Name = "Client", NormalizedName = "CLIENT" };
            var adminRole = new Role { Name = "Admin", NormalizedName = "ADMIN" };

            roleManager.CreateAsync(clientRole).Wait();
            roleManager.CreateAsync(adminRole).Wait();

            var clientUser = new User
            {
                UserName = "clientuser",
                Email = "client@example.com",
                EmailConfirmed = true
            };
            var adminUser = new User
            {
                UserName = "adminuser",
                Email = "Test123*",
                EmailConfirmed = true
            };

            userManager.CreateAsync(clientUser, "Client123!").Wait();
            userManager.CreateAsync(adminUser, "Admin123!").Wait();

            userManager.AddToRoleAsync(clientUser, clientRole.Name).Wait();
            userManager.AddToRoleAsync(adminUser, adminRole.Name).Wait();
        }

    }
}
