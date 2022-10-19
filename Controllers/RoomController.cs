using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test.Data;
using test.Logging;
using test.Models;
using test.Models.Dto;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ILogging _logger;
        public RoomController(ILogging logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RoomDTO>> GetAll()
        {
            _logger.Log("Get all available room","info");
            return Ok(RoomStore.rooms);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<RoomDTO> Get(int id)
        {
            if(id == 0)
            {
                _logger.Log("Get Room with id ="  +id, "error");
                return BadRequest();
            }
            var room = RoomStore.rooms.FirstOrDefault(a => a.Id == id);
            if(room == null)
            {
                return NotFound();

            }
            _logger.Log("successfully get Room with id =" + id,"info");
            return Ok(room);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<RoomDTO> CreateRoom([FromBody] RoomDTO roomDTO)
        {
           if(RoomStore.rooms.FirstOrDefault(u=>u.Name.ToLower() == roomDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Room Already Exist");
                return BadRequest(ModelState);
            }
            if(roomDTO == null)
            {
                return BadRequest(roomDTO);
            }
            if(roomDTO.Id> 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            roomDTO.Id = RoomStore.rooms.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            RoomStore.rooms.Add(roomDTO);
            return Ok(roomDTO);
        }

        [HttpDelete("{id:int}",Name ="Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if(id ==0 )
            {
                return BadRequest();
            }
            var room = RoomStore.rooms.FirstOrDefault(u => u.Id == id);
            if(room == null)
            {
                return NotFound();
            }
            RoomStore.rooms.Remove(room);
            return NoContent();
          
        }

        [HttpPut]
        public IActionResult UpdateRoom(int id, [FromBody] RoomDTO roomDTO)
        {

            if (id == 0 || id != roomDTO.Id)
            {
                return BadRequest();
            }
            var room = RoomStore.rooms.FirstOrDefault(u => u.Id == id);
            room.Id  = roomDTO.Id;
            room.Name = roomDTO.Name;
            return NoContent();
        }
    }
}
