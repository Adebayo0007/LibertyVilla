using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class RoomOrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; } 
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set;}
        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get;set; }
        [Required]
        public long TotalCost { get; set; }
        [Required]
        public int RoomId { get; set; }
        public bool IsPaymentsuccessful { get; set; } = false;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string Phone { get; set; } = default!;
        [ForeignKey("RoomId")]
        public HotelRoom HotelRoom { get; set; }
        public string Status { get; set; } = default!;
    }
}
