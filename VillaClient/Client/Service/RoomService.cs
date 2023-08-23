using Intersoft.Crosslight.Mobile;
using Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using VillaClient.Client.Service.Iservice;

namespace VillaClient.Client.Service
{
    public class RoomService : IRoomService
    {
        private readonly HttpClient _client;
        public RoomService(HttpClient client)
        {
            _client = client;
        }

        public async Task<HotelRoomDto> GetRoomDetails(int id, DateTime startingDate, DateTime endingDate)
        {
           
            var response = await _client.GetAsync($"api/hotelroom/gethotelroomdetails?roomId={id}&checkinDate={startingDate}&checkoutDate={endingDate}");

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<HotelRoomDto>(contentTemp);
                return result;
            }
            else
            {
                
                throw new HttpRequestException($"Failed to retrieve rooms. Status code: {response.StatusCode}");
            }
        }

        public async Task<IEnumerable<HotelRoomDto>> GetRooms()
        {
            var response = await _client.GetAsync("api/hotelroom/gethotelrooms");

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDto>>(contentTemp);
                return result;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve rooms. Status code: {response.StatusCode}");
            }
        }
        public async Task<IEnumerable<HotelRoomDto>> GetRoom(DateTime startingDate, DateTime endingDate)
        {
            var response = await _client.GetAsync($"api/hotelroom/gethotelroom?checkinDate={startingDate}&checkoutDate={endingDate}");

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDto>>(contentTemp);
                return result;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve rooms. Status code: {response.StatusCode}");
            }
        }
    }
}
