﻿using Business.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace LibertyVilla_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles ="SuperAdmin")] 
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;
        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }
        [HttpGet("GetHotelRooms")]
        public async Task<IActionResult> GetHotelRooms()
        {
            var rooms = await _hotelRoomRepository.GetAllHotelRoom();
            return Ok(rooms);
        }
        [HttpGet("GetHotelRoom/{roomId}")]
        public async Task<IActionResult> GetHotelRoom([FromRoute] int? roomId)
        {
            if(roomId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Tittle= "",
                    ErrorMessage ="Invalid Room Id",
                    StatusCode = StatusCodes.Status400BadRequest

                });
            }
            var room = await _hotelRoomRepository.GetHotelRoom(roomId.Value);

            if (room == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Tittle = "",
                    ErrorMessage = "Invalid Room Id",
                    StatusCode = StatusCodes.Status404NotFound

                });
            }
            return Ok(room);
        }
    }
}