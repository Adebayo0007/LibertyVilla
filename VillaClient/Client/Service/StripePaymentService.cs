using Models;
using Newtonsoft.Json;
using System.Text;
using VillaClient.Client.Service.Iservice;

namespace VillaClient.Client.Service
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly HttpClient _client;
        public StripePaymentService(HttpClient client)
        {
            _client = client;
        }
        public async Task<SuccessModel> CheckOut(StripePaymentDto stripePaymentDto)
        {
            var content = JsonConvert.SerializeObject(stripePaymentDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/stripepayment/create", bodyContent);
          
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SuccessModel>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorMessage.ErrorMessage);
            }
            
        }
    }
}
