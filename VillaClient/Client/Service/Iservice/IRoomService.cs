using Models;

namespace VillaClient.Client.Service.Iservice
{
    public interface IRoomService
    {
        Task<IEnumerable<HotelRoomDto>> GetRooms();
        Task<IEnumerable<HotelRoomDto>> GetRoom(DateTime startingDate, DateTime endingDate);
        Task<HotelRoomDto> GetRoomDetails(int id, DateTime startingDate, DateTime endingDate);
    }
}
