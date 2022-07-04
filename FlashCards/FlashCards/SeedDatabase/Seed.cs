using FlashCards.DataAccess;
using FlashCards.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.SeedDatabase
{
    public static class Seed
    {
        public static void SeedDatabase(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();


                context.Database.Migrate();

                if (!userManager.Users.Any())
                {
                    // create some roles

                    var roles = new List<Role>
                {
                    new Role{Name = "Admin"},
                    new Role{Name = "Moderator"},
                    new Role{Name = "Reader"}
                };

                    foreach (var role in roles)
                    {
                        roleManager.CreateAsync(role).Wait();
                    }


                    // create admin user
                    var adminUser = new User
                    {
                        UserName = "Akazad",
                        FirstName = "Abul",
                        LastName = "Kalam"
                    };

                    var result = userManager.CreateAsync(adminUser, "Password12345").Result;
                    if (result.Succeeded)
                    {
                        var admin = userManager.FindByNameAsync("Akazad").Result;
                        userManager.AddToRolesAsync(admin, new[] { "Admin" }).Wait();
                    }
                }
            }
        }
    }
}
