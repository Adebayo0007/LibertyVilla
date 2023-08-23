using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using VillaClient.Client.Service.Iservice;

namespace VillaClient.Client.Service
{
    public class RoomOrderDetailsService : IRoomOrderDetailsService
    {
        private readonly HttpClient _client;
        public RoomOrderDetailsService(HttpClient client)
        {
            _client = client;
        }

        public async Task<RoomOrderDetailDto> CreateOrder(RoomOrderDetailDto roomOrderDetailDto)
        {
           // _client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = JsonConvert.SerializeObject(roomOrderDetailDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/roomorder/createorder", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RoomOrderDetailDto>(contentTemp);

            if (!response.IsSuccessStatusCode)
            { 
                return new RoomOrderDetailDto {Status = response.RequestMessage.ToString()} ;
            }
            else
            {
                return result;
            }
        }

        public Task<RoomOrderDetailDto> MarkPaymentSuccesful(RoomOrderDetailDto roomOrderDetailDto)
        {
            throw new NotImplementedException();
        }

        public Task<RoomOrderDetailDto> SaveRoomOrderDetails(RoomOrderDetailDto roomOrderDetailDto)
        {
            throw new NotImplementedException();
        }
    }
}
