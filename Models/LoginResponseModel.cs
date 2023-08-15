using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; } 
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; } = false;
        public ApplicationUserDto? ApplicationUserDto { get; set; } 
    }
}
