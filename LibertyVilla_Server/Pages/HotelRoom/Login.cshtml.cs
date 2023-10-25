using Models;
using Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components;
using Business.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace LibertyVilla_Server.Pages.HotelRoom
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly NavigationManager _navigationManager;
        private readonly IApplicationUserRepository _applicationUserRepository;
        public LoginModel(NavigationManager navigationManager, IApplicationUserRepository applicationUserRepository)
        {
            _navigationManager = navigationManager; 
            _applicationUserRepository = applicationUserRepository;
            
        }
        public string ReturnUrl { get; set; } 
      
        public async Task<IActionResult>
            OnGetAsync(string paramUsername, string paramPassword)
        {
            string returnUrl = Url.Content("~/");
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            // *** !!! This is where user is validated !!! ***
            
            if (paramUsername == null || paramPassword == null)
                throw new ArgumentNullException(nameof(LoginRequestModel));

            try
            {
                var userDetail = await _applicationUserRepository.GetUserByEmail(paramUsername);
                //var existByPassword = BCrypt.Net.BCrypt.Verify(paramPassword, userDetail?.Password);
                var existByPassword = BCrypt.Net.BCrypt.Verify(paramPassword, userDetail?.Password);
                if (userDetail == null || existByPassword == false)
                    throw new ArgumentNullException(nameof(LoginRequestModel));
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userDetail.Id.ToString()),
                    new Claim(ClaimTypes.Name, userDetail.Name),
                    new Claim(ClaimTypes.Email, userDetail.Email),
                    new Claim(ClaimTypes.Role, userDetail.Role),
                };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };
                try
                {
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                    return userDetail.Role.Equals(SD.Role_SuperAdmin) ?
                               LocalRedirect($"{returnUrl}hotel-room") :
                               LocalRedirect($"{returnUrl}");

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
