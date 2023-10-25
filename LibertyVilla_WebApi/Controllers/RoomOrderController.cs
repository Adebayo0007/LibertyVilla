using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using NLog;
using System.Security.Claims;

namespace LibertyVilla_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomOrderController : ControllerBase
    {
        private readonly IRoomOrderRepository _roomOrderRepository;
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public RoomOrderController(IRoomOrderRepository roomOrderRepository)
        {
            _roomOrderRepository = roomOrderRepository;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] RoomOrderDetailDto roomOrderDetailDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Error($"invalid request model for creating reservation for room id {roomOrderDetailDto.RoomId}");
                    string response1 = "Invalid input,check your input very well";
                    return BadRequest(new { message = response1 });
                }
                logger.Info($"start creating reservation for room id {roomOrderDetailDto.RoomId}");
                // roomOrderDetailDto.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)  //this should be handled after implementing authentication
                roomOrderDetailDto.UserId = 1;
                var roomOrderDto = await _roomOrderRepository.Create(roomOrderDetailDto);
                if (roomOrderDto is null)
                {
                    logger.Warn($"could not return the room order details while creating reservation for room id {roomOrderDetailDto.RoomId}");
                    return BadRequest();
                }
                logger.Info($"successfully created  room id {roomOrderDetailDto.RoomId} reservation for user named {roomOrderDetailDto.Name}");
                return Ok(roomOrderDto);
            }
            catch (Exception ex)
            {
                logger.Error($"Exception Type {ex.GetType()} and the exception message says {ex.Message}. An error occured while creating reservation for room id {roomOrderDetailDto.RoomId} for user named {roomOrderDetailDto.Name}");
                throw;
            }
        }

        [HttpGet("UpdateToPaidSuccessfully")]
        public async Task<IActionResult> UpdateToPaidSuccessfully(int? roomId)
        {
           
            if(roomId != null)
            {
                try
                {
                   var response =   await  _roomOrderRepository.MarkPaymentSuccessful(roomId.Value);
                    string redirectUrl = "https://localhost:7212/transactionsuccessful";
                    return Redirect(redirectUrl);
                }
                catch(Exception ex)
                {
                    throw;
                }
                
            }
            
            return BadRequest();
        }
    }
}
