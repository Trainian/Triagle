using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class IdentityWebContextSeed
    {
        public static async Task SeedAsync(IdentityWebContext context, UserManager<WebUser> user, RoleManager<IdentityRole> role)
        {
            if (!await context.Users.AnyAsync())
            {
                var admin = new WebUser() { UserName = "Trainian", Email = "Dimon007@inbox.ru", EmailConfirmed = true, Avatar = "3.png" };
                await user.CreateAsync(admin, "123456");

                var roleAdmin = new IdentityRole("Administrator");
                await role.CreateAsync(roleAdmin);

                await user.AddToRoleAsync(admin, "Administrator");
            }
            if(await context.Roles.FirstOrDefaultAsync(r => r.Name == "User") == null)
            {
                var roleUser = new IdentityRole("User");
                await role.CreateAsync(roleUser);
            }
        }
    }
}
