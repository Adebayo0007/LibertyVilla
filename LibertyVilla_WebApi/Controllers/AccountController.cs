using Business.Repository.IRepository;
using LibertyVilla_WebApi.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using NLog;

namespace LibertyVilla_WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IJWTAuthentication _authentication;
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public AccountController(IApplicationUserRepository applicationUserRepository, IJWTAuthentication authentication)
        {
            _applicationUserRepository = applicationUserRepository;
            _authentication = authentication;
        }

          [HttpPost]
           public async Task<IActionResult> Signin([FromBody] LoginRequestModel loginModel)
           {
                try
                {

                    if (!ModelState.IsValid)
                    {
                        string response1 = "Invalid input,check your input very well";
                        logger.Error($"invalid request model when user with email  {loginModel.Email?? "not provided"} is trying to sign in");   
                       return BadRequest(new { message = response1 });
                    }
                    if (string.IsNullOrWhiteSpace(loginModel.Email) || string.IsNullOrWhiteSpace(loginModel.Password))
                    {
                       logger.Error($"invalid request model when user with email  {loginModel.Email ?? "not provided"} is trying to sign in,either of the mail or password is not provided");
                       return BadRequest();
                    }
                    var loginResponseModel = await _applicationUserRepository.Signin(loginModel);
                    if (loginResponseModel is null)
                    {
                      logger.Error($"user with email {loginModel.Email ?? "not provided"} is not authenticated");
                      return Unauthorized();
                    }
                    if (!loginResponseModel.IsSuccessful)
                    {
                      logger.Error($"user with email {loginModel.Email ?? "not provided"} is not authenticated successfully");
                      return BadRequest(loginResponseModel);
                    }

                    var token = _authentication.GenerateToken(loginResponseModel);
                    var refreshToken = _authentication.GenerateRefreshToken();
                    var response = await _applicationUserRepository.UpdateRefreshToken(loginResponseModel.ApplicationUserDto.Email, refreshToken);
                    response.Token = token;
                    response.Message = "Logged in successful";
                    response.IsSuccessful = true;
                    logger.Info($"user with email {loginModel.Email} is authenticated successfully");
                    return Ok(response);
                }
                catch (Exception ex) 
                {
                  logger.Error($"Exception Type {ex.GetType()} and Exception message says {ex.Message} while {loginModel.Email} is trying to login into the Application");
                  throw;
                }
           }
        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] ApplicationUserDto requestModel)
        {
            try
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
            catch(Exception ex)
            {
                logger.Error($"Exception Type {ex.GetType()} and Exception message says {ex.Message} while {requestModel.Email} is trying to register into the Application");
                throw;
            }
        }
    }
}
