using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApplicationUserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;
        public string? Role { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = default!;
        [Required(ErrorMessage = "Phone number is required")]
        public string? PhoneNumber { get; set; } = default!;
        public string? RefreshToken { get; set; }
        public bool IsSuccessful { get; set; } = false;
    }
}
