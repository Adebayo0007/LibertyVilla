using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class HotelRoom 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public double RegularRate { get; set; }
        public string Details { get; set; } = default!;
        public string Sqft { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public virtual ICollection<HotelRoomImage> HotelRoomImages{ get; set; }
    }
}
