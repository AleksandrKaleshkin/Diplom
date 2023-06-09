using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebTraining.Core.Interfaces;
using WebTraining.Core.Interfaces.IMeasurements;
using WebTraining.Core.Services;
using WebTraining.Core.Services.MeasurementsService;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;
using WebTraining.DB.Models.InitializeData;
using WebTraining.DB.Models.Measurements;
using WebTraining.DB.Repositories;
using WebTraining.DB.Repositories.MeasurementsRepository;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string? connection = builder.Configuration.GetConnectionString("Connection");

        builder.Services.AddScoped<IExerciseRepository<Exercise>, ExerciseRepository>();
        builder.Services.AddScoped<IExerciseService, ExerciseService>();

        builder.Services.AddScoped<ITrainingRepository<Training>, TrainingRepository>();
        builder.Services.AddScoped<ITrainingService, TrainingService>();

        builder.Services.AddScoped<ITERepository<TrainingExercise>, TrainingExerciseRepository>();
        builder.Services.AddScoped<ITrainingExerciseService,TrainingExerciseService>();

        builder.Services.AddScoped<INotepadRepository<Notepad>, NotepadRepository>();
        builder.Services.AddScoped<INotepadService, NotepadService>();

        builder.Services.AddScoped<IDoubleMeasRepository<DoubleMeasurements>, DoubleMeasurementsRepository>();
        builder.Services.AddScoped<IDoubleMeasurementsService, DoubleMeasurementsService>();

        builder.Services.AddScoped<ISingleMeasRepository<SingleMeasurements>, SingleMeasurementsRepository>();
        builder.Services.AddScoped<ISingleMeasurementstService, SingleMeasurementsService>();

        builder.Services.AddDbContext<WebTrainingContext>(options => options.UseNpgsql(connection));

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LogoutPath = new PathString("/Account/Login");
            });



        builder.Services.AddIdentity<User, IdentityRole>(opts => {
            opts.User.AllowedUserNameCharacters= ".@1234567890abcdefghijklmnopqrstuvwxyz";
            opts.User.RequireUniqueEmail = true;
            opts.SignIn.RequireConfirmedAccount = false;
            opts.Password.RequiredLength = 5;   // минимальная длина
            opts.Password.RequireNonAlphanumeric = true;   // требуются ли не алфавитно-цифровые символы
            opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
            opts.Password.RequireUppercase = true; // требуются ли символы в верхнем регистре
            opts.Password.RequireDigit = true; // требуются ли цифры
        }).AddEntityFrameworkStores<WebTrainingContext>();

        builder.Services.Configure<SecurityStampValidatorOptions>(options =>
        {
            options.ValidationInterval = TimeSpan.FromSeconds(20);
        });



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
                var signInmanager = services.GetRequiredService<SignInManager<User>>();
                await RoleInitializer.InitializeAsync(userManager, rolesManager, signInmanager);


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


        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();            

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}