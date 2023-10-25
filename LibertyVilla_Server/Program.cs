using Business.Repository.IRepository;
using Common;
using DataAccess.Data;
using LibertyVilla_Server.Data;
using LibertyVilla_Server.Service;
using LibertyVilla_Server.Service.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NLog;

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

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();


            builder.Services.AddRazorPages();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
            builder.Services.AddScoped<IHotelImageRepository, HotelImageRepository>();
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            builder.Services.AddScoped<IFileUpload, FileUpload>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<HttpContextAccessor>();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<HttpClient>();
           


            var app = builder.Build();
            
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            // BLAZOR COOKIE Auth Code (begin)
            app.UseCookiePolicy();
            // BLAZOR COOKIE Auth Code (end)
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            // BLAZOR COOKIE Auth Code (begin)
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            // BLAZOR COOKIE Auth Code (end)

            app.Run();
        }
    }
}