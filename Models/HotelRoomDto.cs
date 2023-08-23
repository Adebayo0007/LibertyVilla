using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HotelRoomDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; } = default!;
        [Range(1, 100, ErrorMessage = "The occupancy must be between 1 and 100")]
        public int Occupancy { get; set; }
        [Range(1,1000,ErrorMessage = "The regular rate must be between $1 and $1000")]
        public double RegularRate { get; set; }
        public string Details { get; set; } = default!;
        public string Sqft { get; set; } = default!;
        public int TotalDays { get; set; }
        public bool IsBooked { get; set; } = false;
        public virtual ICollection<HotelRoomImageDto> HotelRoomImages { get; set; }
        public List<string> ImagesUrls { get; set; }
    }
}
