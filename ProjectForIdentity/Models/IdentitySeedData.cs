using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProjectForIdentity.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "Admin_123";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();

            if (context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new AppUser
                {
                    FullName="azizinfulladı",
                    UserName = adminUser,
                    Email = "admin@aziz.com",
                    PhoneNumber = "05347926030"
                };
                await userManager.CreateAsync(user,adminPassword);
            }
        }
    }
}
