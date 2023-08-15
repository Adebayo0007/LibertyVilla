using DataAccess.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationUser 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        [Required]
        public string Role { get; set; }
        public string Gender { get; set; } = default!;
        [Required]
        public string PhoneNumber { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime DateCreated { get; set; }   = DateTime.UtcNow;
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
