using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VillaClient.Client;
using VillaClient.Client.Service;
using VillaClient.Client.Service.Iservice;
using Microsoft.Extensions.Caching.Memory;

namespace VillaClient.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddMemoryCache();
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

           builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseUrl")) });
           // builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IRoomService, RoomService>(); 
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IRoomOrderDetailsService, RoomOrderDetailsService>();
            builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();

            await builder.Build().RunAsync();
        }
    }
}