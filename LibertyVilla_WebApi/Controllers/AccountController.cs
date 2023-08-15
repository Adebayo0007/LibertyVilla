using Business.Repository.IRepository;
using LibertyVilla_WebApi.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace LibertyVilla_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IJWTAuthentication _authentication;
        public AccountController(IApplicationUserRepository applicationUserRepository, IJWTAuthentication authentication)
        {
            _applicationUserRepository = applicationUserRepository;
            _authentication = authentication;
        }

          [HttpPost]
           public async Task<IActionResult> Signin([FromBody] LoginRequestModel loginModel)
           {
               if (!ModelState.IsValid)
               {
                   string response1 = "Invalid input,check your input very well";
                   return BadRequest(new { message = response1 });
               }
               if (string.IsNullOrWhiteSpace(loginModel.Email) || string.IsNullOrWhiteSpace(loginModel.Password))
               {
                   return NotFound();
               }
               var loginResponseModel = await _applicationUserRepository.Signin(loginModel);
               if (loginResponseModel is null)
               {
                   return Unauthorized();
               }
               if (!loginResponseModel.IsSuccessful)
               {
                   return BadRequest(loginResponseModel);
               }

               var token = _authentication.GenerateToken(loginResponseModel);
               var refreshToken = _authentication.GenerateRefreshToken();
               var response = await _applicationUserRepository.UpdateRefreshToken(loginResponseModel.ApplicationUserDto.Email, refreshToken);
               response.Token = token;
               response.Message = "Logged in successful";
               response.IsSuccessful = true;
               return Ok(response);
           }
        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] ApplicationUserDto requestModel)
        {
            if (!ModelState.IsValid)
            {
                string response1 = "Invalid input,check your input very well";
                return BadRequest(new { message = response1 });
            }
            var userIsUnique = await _applicationUserRepository.UserIsUnique(requestModel.Email);
            if (userIsUnique) return BadRequest(new { message = "User already exist"});
            var applicationUserDto = await _applicationUserRepository.Signup(requestModel);
            if (applicationUserDto is null)
            {
                return Unauthorized();
            }
            return Ok(applicationUserDto);
        }
    }
}
