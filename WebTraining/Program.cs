using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebTraining.Core.Interfaces;
using WebTraining.Core.Services;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;
using WebTraining.DB.Models.InitializeData;
using WebTraining.DB.Repositories;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string? connection = builder.Configuration.GetConnectionString("Connection");
        builder.Services.AddTransient<IExerciseService, ExerciseService>();
        builder.Services.AddTransient<ITrainingService, TrainingService>();
        builder.Services.AddTransient<ITrainingExerciseService,TrainingExerciseService>();
        builder.Services.AddTransient<INotepadService, NotepadService>();
        builder.Services.AddTransient<IUnitOfWork, EFUnitOfWork>();
        builder.Services.AddDbContext<WebTrainingContext>(options => options.UseNpgsql(connection));



        builder.Services.AddIdentity<User, IdentityRole>(opts => {
            opts.SignIn.RequireConfirmedAccount = false;
            opts.Password.RequiredLength = 5;   // минимальная длина
            opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
            opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
            opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
            opts.Password.RequireDigit = true; // требуются ли цифры
        }).AddEntityFrameworkStores<WebTrainingContext>();



        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();



        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleInitializer.InitializeAsync(userManager, rolesManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}