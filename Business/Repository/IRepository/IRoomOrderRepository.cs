using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IRoomOrderRepository
    {
        public Task<RoomOrderDetailDto> Create(RoomOrderDetailDto roomOrderDetailDto);
        public Task<RoomOrderDetailDto> MarkPaymentSuccessful(int id);
        public Task<RoomOrderDetailDto> GetRoomOrderDetail(int roomOrderId);
        public Task<IEnumerable<RoomOrderDetailDto>> GetAllRoomOrdersDetails();
        public Task<bool> UpdateOrderStatus(int roomOrderId, string status);
       
    }
}
