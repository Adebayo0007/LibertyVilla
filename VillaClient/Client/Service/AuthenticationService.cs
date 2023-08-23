using Intersoft.Crosslight.Mobile;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using IAuthenticationService = VillaClient.Client.Service.Iservice.IAuthenticationService;

namespace VillaClient.Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthStateProvider _authStateProvider;
        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthStateProvider authStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }
        public async Task<LoginResponseModel> Login(LoginRequestModel model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponseModel>(contentTemp);

            if (result.IsSuccessful)
            {
                 //await _localStorage.SetPropertyValue(SD.Local_Token, result.Token);
                //await _localStorage.SetPropertyValue(SD.Local_User_Details, result.ApplicationUserDto);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new LoginResponseModel { IsSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            //await _localStorage.RemoveItemAsync(SD.Local_Token);
            //await _localStorage.RemoveItemAsync(SD.Local_User_Details);
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<ApplicationUserDto> RegisterUser(ApplicationUserDto userDto)
        {

            var content = JsonConvert.SerializeObject(userDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signup", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApplicationUserDto>(contentTemp);

            if (result.IsSuccessful)
            {
                return new ApplicationUserDto { IsSuccessful = true };
            }
            else
            {
                return result;
            }
        }
    }
}
