using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RoomOrderDetailDto
    {
        public int Id { get; set; }
        //[Required]
        public int UserId { get; set; } 
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }
        [Required]
        public long TotalCost { get; set; }
        [Required]
        public int RoomId { get; set; }
        public bool IsPaymentsuccessful { get; set; } = false;
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Pnone number is required")]
        public string Phone { get; set; } = default!;
        public HotelRoomDto HotelRoomDto { get; set; }
        public string Status { get; set; }
    }
}
