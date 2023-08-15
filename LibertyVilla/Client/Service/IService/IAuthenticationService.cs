namespace LibertyVilla.Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<LoginResponseModel> Login(LoginRequestModel model);
        Task<ApplicationUserDto> RegisterUser(ApplicationUserDto userDto);
        Task Logout();
    }
}
