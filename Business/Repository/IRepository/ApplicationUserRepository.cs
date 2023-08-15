using AutoMapper;
using Common;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository.IRepository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ApplicationUserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDto> Signup(ApplicationUserDto userDto)
        {
            ApplicationUser user = _mapper.Map<ApplicationUserDto, ApplicationUser>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Role = SD.Role_Customer;
            var userr = await _db.ApplicationUsers.AddAsync(user);
            await _db.SaveChangesAsync();
            return _mapper.Map<ApplicationUser, ApplicationUserDto>(userr.Entity);
            
        }

        public async Task<int> DeleteUser(int userId)
        {

            try
            {
                var userDetails = await _db.ApplicationUsers.FindAsync(userId);
                if (userDetails != null)
                {
                    _db.ApplicationUsers.Remove(userDetails);
                    return await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        public async Task<ApplicationUserDto> GetUserByEmail(string userEmail)
        {
            try
            {
                var userDetails = await _db.ApplicationUsers.SingleOrDefaultAsync(u =>u.Email == userEmail);
                if (userDetails != null)
                {
                    return _mapper.Map<ApplicationUser, ApplicationUserDto>(userDetails);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return null;
        }

        public async Task<ApplicationUserDto> GetUserById(int userId)
        {
            try
            {
                var userDetails = await _db.ApplicationUsers.SingleOrDefaultAsync(u => u.Id == userId);
                if (userDetails != null)
                {
                    return _mapper.Map<ApplicationUser, ApplicationUserDto>(userDetails);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return null;
        }

        public async Task<ApplicationUserDto> UpdateUser(int userId, ApplicationUserDto user)
        {
            try
            {
                var userDetails = await _db.ApplicationUsers.SingleOrDefaultAsync(u => u.Id == userId);
                if (userDetails != null)
                {
                    userDetails.Name = user.Name ?? userDetails.Name;
                    userDetails.PhoneNumber = user.PhoneNumber ?? userDetails.PhoneNumber;
                    userDetails.Email = user.Email ?? userDetails.Email;
                    userDetails.Password = user.Password ?? userDetails.Password;
                    userDetails.Gender = user.Gender ?? userDetails.Gender;
                    _db.ApplicationUsers.Update(userDetails);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<ApplicationUser, ApplicationUserDto>(userDetails);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return null;
        }

        public async Task<LoginResponseModel> Signin(LoginRequestModel loginRequestModel)
        {
            var email = await _db.ApplicationUsers.AnyAsync(u => u.Email == loginRequestModel.Email);

            var user = await _db.ApplicationUsers.SingleOrDefaultAsync(u => u.Email == loginRequestModel.Email);
            if(user == null)
            {
                return new LoginResponseModel
                {
                    Message = "User does not exist 🙄",
                    IsSuccessful = false
                };
            }
            if (user.IsActive == false)
            {
                return new LoginResponseModel
                {
                    Message = "You are not an active user 🙄",
                    IsSuccessful = false
                };
            }        
            var password = BCrypt.Net.BCrypt.Verify(loginRequestModel.Password, user.Password);

            if (email == false || password == false)
            {
                return new LoginResponseModel
                {
                    Message = "Invalid mail or password 🙄",
                    IsSuccessful = false
                };
            }


            ApplicationUserDto userDto = null;

            if (user is not null)
            {
                userDto = _mapper.Map<ApplicationUser, ApplicationUserDto>(user);
            }

            return new LoginResponseModel
            {
                Message = "Login successfully 😎",
                IsSuccessful = true,
                ApplicationUserDto = userDto
            };
        }

        public async Task<LoginResponseModel> UpdateRefreshToken(string userEmail, string refreshToken)
        {
            var user = await _db.ApplicationUsers.SingleOrDefaultAsync(u => u.Email == userEmail);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
           await  _db.SaveChangesAsync();
            var responseModel = _mapper.Map<ApplicationUser, ApplicationUserDto>(user);
            return new LoginResponseModel
            {
                Message = "Refresh token updated successfully",
                IsSuccessful = true,
                RefreshToken = refreshToken,
                ApplicationUserDto = responseModel

            };
        }
        public async  Task<bool> UserIsUnique(string email)
        {
            return await _db.ApplicationUsers.AnyAsync(u => u.Email == email); 
        }
    }
}
