using LibertyVilla.Client.Service.IService;
using LibertyVilla.Client.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace LibertyVilla.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
              var builder = WebAssemblyHostBuilder.CreateDefault(args);
              builder.RootComponents.Add<App>("#app");
              builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseUrl"))});
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            await builder.Build().RunAsync();
        }
    }
}