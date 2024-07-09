using Microsoft.AspNetCore.Identity;
using SummerPracticePaul.Models;

namespace SummerPracticePaul.Repository.Data
{
    public static class Extensions
    {
        public static void CreateDbIfNotExists(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<InMemoryDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();

                context.Database.EnsureCreated();
                DataSeeder.Initialize(context, userManager, roleManager);
            }
        }
    }
}
