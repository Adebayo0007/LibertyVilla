using Business.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using NLog;
using System.Linq.Expressions;

namespace LibertyVilla_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles ="SuperAdmin")] 
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }
        [HttpGet("GetHotelRooms")]
        public async Task<IActionResult> GetHotelRooms()
        {
            try
            {
                logger.Info("start loading hotel rooms");
                var rooms = await _hotelRoomRepository.GetAllHotelRoom();
                logger.Info("successful loading the hotel rooms");
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                logger.Error($"Exception Type {ex.GetType()} and the exception message says {ex.Message}.An error occured while loading hotel rooms ");
                throw;
            }
        }
        [HttpGet("GetHotelRoom")]
        public async Task<IActionResult> GetHotelRoom(DateTime checkinDate,DateTime checkoutDate)
        {
            try
            {
                logger.Info($"start loading hotel rooms from {checkinDate} to {checkoutDate}");
                var rooms = await _hotelRoomRepository.GetAvailableHotelRoom(checkinDate, checkoutDate);
                logger.Info($"successful loading the hotel rooms from {checkinDate} to {checkoutDate}");
                return Ok(rooms);
            }
            catch (Exception ex) 
            {
                logger.Error($"Exception Type {ex.GetType()} and the exception message says {ex.Message}.An error occured while loading for hotels available from {checkinDate} to {checkoutDate}");
                throw;
            }
           
        }
        [HttpGet("GetHotelRoomDetails")]
        public async Task<IActionResult> GetHotelRoomDetails(int? roomId, DateTime? checkinDate, DateTime? checkoutDate)
        {
            try
            {
                logger.Info($"start loading hotel room of id {roomId} from {checkinDate} to {checkoutDate}");
                if (roomId == null)
                {
                    logger.Warn($"the id is null");

                    return BadRequest(new ErrorModel()
                    {
                        Tittle = "",
                        ErrorMessage = "Invalid Room Id",
                        StatusCode = StatusCodes.Status400BadRequest

                    });
                }
                var room = await _hotelRoomRepository.GetHotelRoom(roomId.Value, checkinDate, checkoutDate);

                if (room == null)
                {
                    logger.Warn($"a room of id {roomId} from {checkinDate} to {checkoutDate} can't be found");
                    return BadRequest(new ErrorModel()
                    {
                        Tittle = "",
                        ErrorMessage = "Invalid Room Id",
                        StatusCode = StatusCodes.Status404NotFound

                    });
                } 
                logger.Info($"successful loading the hotel room of id {roomId} from {checkinDate} to {checkoutDate}");
                return Ok(room);
            }
            catch (Exception ex)
            {
                logger.Error($"Exception Type {ex.GetType()} and the exception message says {ex.Message}.An error occured while loading for hotel of id {roomId} from {checkinDate} to {checkoutDate}");
                throw;
            }

          
        }
    }
}
