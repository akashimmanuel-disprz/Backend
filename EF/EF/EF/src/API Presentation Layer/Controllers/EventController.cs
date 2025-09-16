using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.DTOs;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/event
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // GET: api/event/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var e = await _eventService.GetEventByIdAsync(id);
            if (e == null) return NotFound();
            return Ok(e);
        }

        // POST: api/event
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto eventDto)
        {
            try
            {
                var createdEvent = await _eventService.CreateEventAsync(eventDto);
                return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.EventId }, createdEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // validation or collision error
            }
        }

        // PUT: api/event/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventUpdateDto eventDto)
        {
            if (id != eventDto.EventId)
                return BadRequest("ID mismatch.");

            try
            {
                var updatedEvent = await _eventService.UpdateEventAsync(eventDto);
                if (updatedEvent == null) return NotFound();
                return Ok(updatedEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/event/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var deleted = await _eventService.DeleteEventAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
