
using Models;
using Refit;

namespace VillaClient.Client.Service.Iservice
{
    public interface IRefitRequest
    {
         [Post("/api/roomorder/createorder")]
         public Task<RoomOrderDetailDto> CreateOrder([Body]RoomOrderDetailDto roomOrderDetailDto);
    }
}
