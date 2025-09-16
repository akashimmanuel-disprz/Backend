using DomainCoreLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IEventRepository
    {
        Task<List<EventModel>> GetAllEventsAsync();
        Task<EventModel?> GetEventByIdAsync(int eventId);
        Task<EventModel> AddEventAsync(EventModel newEvent);
        Task<EventModel?> UpdateEventAsync(EventModel updatedEvent);
        Task<bool> DeleteEventAsync(int id);   
    }
}