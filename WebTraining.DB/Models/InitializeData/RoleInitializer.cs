using Microsoft.AspNetCore.Identity;

namespace WebTraining.DB.Models.InitializeData
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Почитать как поместить в конфигурацию и затем передать в метод
            string adminEmail = "admin@mail.ru";
            string adminPassword = "Admin@!12345";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("athlete") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("athlete"));
            }
            if (await roleManager.FindByNameAsync("coach") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("coach"));
            }
            if (await userManager.FindByNameAsync(adminEmail)==null)
            {
                User admin = new User { UserName = adminEmail, Email= adminEmail, Name="Admin" };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
