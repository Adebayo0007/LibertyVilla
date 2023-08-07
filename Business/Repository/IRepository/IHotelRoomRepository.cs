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
        Task<HotelRoomDto> GetHotelRoom(int roomId);
        Task<int> DeleteHotelRoom(int roomId);
        Task<IEnumerable<HotelRoomDto>> GetAllHotelRoom();
        Task<HotelRoomDto> RoomIsUnique(string roomName, int roomId = 0);
    }
}
