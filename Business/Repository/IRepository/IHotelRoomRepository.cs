using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        Task<HotelRoomDto> CreateHotelRoon(HotelRoomDto hotelRoomDto);
        Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDto);
        Task<HotelRoomDto> GetHotelRoom(int roomId, DateTime? startDate = null, DateTime? endDate = null);
        Task<int> DeleteHotelRoom(int roomId);
        Task<IEnumerable<HotelRoomDto>> GetAllHotelRoom();
        Task<IEnumerable<HotelRoomDto>> GetAvailableHotelRoom(DateTime startDate, DateTime endDate);
        Task<HotelRoomDto> RoomIsUnique(string roomName, int roomId = 0);
        public Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkoutDate);
    }
}
