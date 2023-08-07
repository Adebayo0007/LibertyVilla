using Business.Repository.IRepository;
using DataAccess.Data;
using LibertyVilla_Server.Data;
using LibertyVilla_Server.Service;
using LibertyVilla_Server.Service.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Migrations;

namespace LibertyVilla_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connection = builder.Configuration.GetConnectionString("LibertyVillaConnectionString");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)
          ));
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
            builder.Services.AddScoped<IHotelImageRepository, HotelImageRepository>();
           // builder.Services.AddScoped<IDbInitializer, IDbInitializer>();
            builder.Services.AddScoped<IFileUpload, FileUpload>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}