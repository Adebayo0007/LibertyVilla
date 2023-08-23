using Models;

namespace VillaClient.Client.Service.Iservice
{
    public interface IAuthenticationService
    {
        Task<LoginResponseModel> Login(LoginRequestModel model);
        Task<ApplicationUserDto> RegisterUser(ApplicationUserDto userDto);
        Task Logout();
    }
}
