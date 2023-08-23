using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace LibertyVilla_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomOrderController : ControllerBase
    {
        private readonly IRoomOrderRepository _roomOrderRepository;
        public RoomOrderController(IRoomOrderRepository roomOrderRepository)
        {
            _roomOrderRepository = roomOrderRepository;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] RoomOrderDetailDto roomOrderDetailDto)
        {
            if (!ModelState.IsValid)
            {
                string response1 = "Invalid input,check your input very well";
                return BadRequest(new { message = response1 });
            }
          
            var roomOrderDto = await _roomOrderRepository.Create(roomOrderDetailDto);
            if (roomOrderDto is null)
            {
                return BadRequest();
            }
            return Ok(roomOrderDto);
        }
    }
}
