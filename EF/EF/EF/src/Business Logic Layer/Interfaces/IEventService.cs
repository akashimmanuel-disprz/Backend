using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventReadDto>> GetAllEventsAsync();
        Task<EventReadDto?> GetEventByIdAsync(int id);
        Task<EventReadDto> CreateEventAsync(EventCreateDto eventDto);
        Task<EventReadDto?> UpdateEventAsync(EventUpdateDto eventDto);
        Task<bool> DeleteEventAsync(int id);
    }
}