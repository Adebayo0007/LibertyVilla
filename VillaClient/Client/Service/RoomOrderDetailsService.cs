using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using Refit;
using System.Text;
//using Microsoft.JSInterop.IJSRuntime;
using VillaClient.Client.Service.Iservice;

namespace VillaClient.Client.Service
{
    public class RoomOrderDetailsService : IRoomOrderDetailsService
    {
        private readonly HttpClient _client;
        private readonly IJSRuntime _jsRuntime;
        public RoomOrderDetailsService(HttpClient client, IJSRuntime jsRuntime)
        {
            _client = client;
            _jsRuntime = jsRuntime;
        }

        public async Task<RoomOrderDetailDto> CreateOrder(RoomOrderDetailDto roomOrderDetailDto)
        {
           /*try
            {
                 IRefitRequest apiService = RestService.For<IRefitRequest>("https://localhost:7086");
                 RoomOrderDetailDto refitResponse = await apiService.CreateOrder(roomOrderDetailDto);
                return refitResponse;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }*/
            

            var content = JsonConvert.SerializeObject(roomOrderDetailDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/roomorder/createorder", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RoomOrderDetailDto>(contentTemp);

            if (!response.IsSuccessStatusCode)
            {
               // await _jsRuntime.InvokeVoidAsync("PaystackFunctions.initiatePayment", "pk_test_fe5a54565a0b6f9a04eb69e5ce395810ea27a2bc", roomOrderDetailDto.TotalCost, "ade@gmail.com");
                //PaystackFunctions.initiatePayment("pk_test_fe5a54565a0b6f9a04eb69e5ce395810ea27a2bc", roomOrderDetailDto.TotalCost, "ade@gmail.com");

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
