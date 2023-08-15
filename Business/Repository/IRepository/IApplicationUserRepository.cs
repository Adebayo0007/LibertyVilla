using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUserDto> Signup(ApplicationUserDto userDto);
        Task<ApplicationUserDto> GetUserById(int userId);
        Task<ApplicationUserDto> GetUserByEmail(string userEmail);
        Task<ApplicationUserDto> UpdateUser(int userId,ApplicationUserDto userDto);
        Task<int> DeleteUser(int userId);
        Task<LoginResponseModel> Signin(LoginRequestModel loginRequestModel);
        Task<bool> UserIsUnique(string email);
        Task<LoginResponseModel> UpdateRefreshToken(string userEmail, string refreshToken);


    }
}
