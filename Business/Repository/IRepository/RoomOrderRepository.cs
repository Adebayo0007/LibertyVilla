using AutoMapper;
using Common;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public class RoomOrderRepository : IRoomOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public RoomOrderRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<RoomOrderDetailDto> Create(RoomOrderDetailDto roomOrderDetailDto)
        {
            try
            {
                roomOrderDetailDto.CheckInDate = roomOrderDetailDto.CheckInDate.Date;
                roomOrderDetailDto.CheckOutDate = roomOrderDetailDto.CheckOutDate.Date;
                var roomOrder = _mapper.Map<RoomOrderDetailDto, RoomOrderDetail>(roomOrderDetailDto);
                roomOrder.Status = SD.Status_Pending;
                var result = await _db.RoomOrderDetails.AddAsync(roomOrder);
                await _db.SaveChangesAsync();
                return _mapper.Map<RoomOrderDetail, RoomOrderDetailDto>(result.Entity);

            }
            catch(Exception ex) 
            {
                return null;
            }
        }

        public async Task<IEnumerable<RoomOrderDetailDto>> GetAllRoomOrdersDetails()
        {
            try
            {
                IEnumerable<RoomOrderDetailDto> roomOrders = 
                    _mapper.Map<IEnumerable<RoomOrderDetail>, IEnumerable<RoomOrderDetailDto>>
                    (_db.RoomOrderDetails.Include(r => r.HotelRoom));
                return roomOrders;
            }
            catch
            {
                return null;
            }
        }

        public async Task<RoomOrderDetailDto> GetRoomOrderDetail(int roomOrderId)
        {
            try
            {
                RoomOrderDetail roomOrder = await _db.RoomOrderDetails
                    .Include(r => r.HotelRoom).ThenInclude(r => r.HotelRoomImages)
                    .FirstOrDefaultAsync(r => r.Id == roomOrderId);

                RoomOrderDetailDto roomOrderDetailsDto = _mapper.Map<RoomOrderDetail, RoomOrderDetailDto>(roomOrder);
                roomOrderDetailsDto.HotelRoomDto.TotalDays = roomOrderDetailsDto.CheckOutDate.Subtract(roomOrderDetailsDto.CheckInDate).Days;
                return roomOrderDetailsDto;


            }
            catch(Exception ex) 
            {
                return null;
            }
        }

      

        public Task<RoomOrderDetailDto> MarkPaymentSuccessful(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatus(int roomOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
