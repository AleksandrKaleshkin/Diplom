using Microsoft.EntityFrameworkCore;
using WebTraining.Core.Interfaces;
using WebTraining.Core.Services;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string? connection = builder.Configuration.GetConnectionString("Connection");
        builder.Services.AddTransient<IExerciseService, ExerciseService>();
        builder.Services.AddTransient<ITrainingService, TrainingService>();
        builder.Services.AddTransient<ITrainingExerciseService,TrainingExerciseService>();
        builder.Services.AddTransient<INotepadService, NotepadService>();
        builder.Services.AddTransient<IUnitOfWork, EFUnitOfWork>();
        builder.Services.AddDbContext<WebTrainingContext>(options => options.UseNpgsql(connection)); 

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

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

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}