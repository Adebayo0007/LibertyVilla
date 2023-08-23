using Models;

namespace VillaClient.Client.Service.Iservice
{
    public interface IRoomOrderDetailsService
    {
        public Task<RoomOrderDetailDto> SaveRoomOrderDetails(RoomOrderDetailDto roomOrderDetailDto);
        public Task<RoomOrderDetailDto> MarkPaymentSuccesful(RoomOrderDetailDto roomOrderDetailDto);
        public Task<RoomOrderDetailDto> CreateOrder(RoomOrderDetailDto roomOrderDetailDto);
    }
}
