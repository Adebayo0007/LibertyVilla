using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Repository.IRepository
{
    public class HotelImageRepository : IHotelImageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelImageRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<int> CreateHotelRoomImage(HotelRoomImageDto imageDto)
        {
            var image = _mapper.Map<HotelRoomImageDto, HotelRoomImage>(imageDto);
            await _db.HotelRoomImages.AddAsync(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
        {
            var image = await _db.HotelRoomImages.FindAsync(imageId);
             _db.HotelRoomImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl)
        {
            var allImages = await _db.HotelRoomImages.FirstOrDefaultAsync(x => x.RoomImageUrl.ToLower() == imageUrl.ToLower());
            _db.HotelRoomImages.Remove(allImages);
            return await _db.SaveChangesAsync();
        }

        public async  Task<int> DeleteHotelRoomImageByRoomId(int roomId)
        {
            var imageList = await _db.HotelRoomImages.Where(h => h.RoomId == roomId).ToListAsync();
            _db.HotelRoomImages.RemoveRange(imageList);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelRoomImageDto>> GetHotelRoomImagesByRoomId(int roomId)
        {
            return _mapper.Map<IEnumerable<HotelRoomImage>, IEnumerable<HotelRoomImageDto>>(await _db.HotelRoomImages.Where(h => h.RoomId == roomId).ToListAsync());
        }
    }
}
