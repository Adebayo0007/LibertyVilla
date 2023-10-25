using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;   
        }
        public async Task<HotelRoomDto> CreateHotelRoon(HotelRoomDto hotelRoomDto)
        {
            var hotelRoom = _mapper.Map<HotelRoomDto, HotelRoom>(hotelRoomDto);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "Ade";
            var addedHotelroom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom, HotelRoomDto>(addedHotelroom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
           
           
            try
            {
                var roomDetails = await _db.HotelRooms.FindAsync(roomId);
                if(roomDetails != null) 
                {
                    var roomImages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
                    //Deleting the model from the database
                      _db.HotelRooms.Remove(roomDetails);
                    //Deleting the images from db
                    _db.HotelRoomImages.RemoveRange(roomImages);
                    return await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex) 
            {
                return 0;
            }
            return 0;
        }

        public async Task<IEnumerable<HotelRoomDto>> GetAllHotelRoom()
        {
         try
            {
                IEnumerable<HotelRoomDto> hotelRoomDto =
                    _mapper.Map<IEnumerable<HotelRoom>,IEnumerable<HotelRoomDto>>(_db.HotelRooms.Include(h => h.HotelRoomImages));
                return hotelRoomDto;
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        public async Task<IEnumerable<HotelRoomDto>> GetAvailableHotelRoom(DateTime startDate, DateTime endDate)
        {
            try
            {
                IEnumerable<HotelRoomDto> hotelRoomDto =
                    _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDto>>(_db.HotelRooms.Include(h => h.HotelRoomImages));
                foreach (HotelRoomDto roomDto in hotelRoomDto)
                {
                    var booked = await IsRoomBooked(roomDto.Id, startDate, endDate);
                    if (booked)
                    {
                        roomDto.IsBooked = true;
                    }
                }
                return hotelRoomDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkoutDate)
        {

            var status = false;
            var existingBooking = await _db.RoomOrderDetails.Where(r => r.RoomId == roomId && r.IsPaymentsuccessful &&
            //check if checkin date that user wants does not fall in between any dates for room that is booked
            ((checkInDate < r.CheckOutDate && checkInDate >= r.CheckInDate) ||
            //check if checkout date that user wants does not fall in between  any dates  for room that is booked
            (checkoutDate.Date > r.CheckInDate.Date && checkInDate.Date <= r.CheckInDate.Date)))
                .FirstOrDefaultAsync();
            if (existingBooking != null)
            {
                status = true;
            }
            return status;
        }

        public async Task<HotelRoomDto> GetHotelRoom(int roomId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                using (var hotelRoom = _db.HotelRooms.Include(h => h.HotelRoomImages).SingleOrDefaultAsync(h => h.Id == roomId))
                {
                    var hotelRoomDto = _mapper.Map<HotelRoom, HotelRoomDto>(await hotelRoom);
                    
                    var booked = await IsRoomBooked(hotelRoomDto.Id, startDate.Value, endDate.Value);
                    if (booked)
                    {
                        hotelRoomDto.IsBooked = true;
                    }
                    return hotelRoomDto;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDto> RoomIsUnique(string roomName, int roomId = 0)
        {
            try
            {
                if(roomId == 0)
                {
                    using (var hotelRoom = _db.HotelRooms.SingleOrDefaultAsync(h => h.Name.ToLower() == roomName.ToLower()))
                    {
                        var hotelRoomDto = _mapper.Map<HotelRoom, HotelRoomDto>(await hotelRoom);
                        return hotelRoomDto;
                    }
                }
                else
                {
                    using (var hotelRoom = _db.HotelRooms.SingleOrDefaultAsync(h => h.Name.ToLower() == roomName.ToLower() && h.Id != roomId))
                    {
                        var hotelRoomDto = _mapper.Map<HotelRoom, HotelRoomDto>(await hotelRoom);
                        return hotelRoomDto;
                    }

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDto)
        {
            try
            {
                if(roomId == hotelRoomDto.Id)
                {
                    //valid
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDto, HotelRoom>(hotelRoomDto, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDto>(updatedRoom.Entity);
                }

            }
            catch(Exception ex)
            {
                //Invalid
                return null;

            }
            return null;
        }
    }
}
