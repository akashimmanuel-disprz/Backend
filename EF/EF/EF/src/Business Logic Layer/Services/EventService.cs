using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DomainCoreLayer.Entities;
using BusinessLogicLayer.Validators;

namespace BusinessLogicLayer.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventReadDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllEventsAsync();

            return events.Select(e => new EventReadDto
            {
                EventId = e.EventId,
                Title = e.Title,
                Description = e.Description,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Recurrence = e.Recurrence == null ? null : new RecurrenceDto
                {
                    RecurrenceType = e.Recurrence.RecurrenceType,
                    RecurrenceCount = e.Recurrence.RecurrenceCount
                }
            });
        }

        public async Task<EventReadDto?> GetEventByIdAsync(int id)
        {
            var e = await _eventRepository.GetEventByIdAsync(id);
            if (e == null) return null;

            return new EventReadDto
            {
                EventId = e.EventId,
                Title = e.Title,
                Description = e.Description,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Recurrence = e.Recurrence == null ? null : new RecurrenceDto
                {
                    RecurrenceType = e.Recurrence.RecurrenceType,
                    RecurrenceCount = e.Recurrence.RecurrenceCount
                }
            };
        }

        public async Task<EventReadDto> CreateEventAsync(EventCreateDto eventDto)
        {
            // Validate insertion (checks collisions with all events)
            EventInsertValidator.EnsureNoCollision(eventDto, await _eventRepository.GetAllEventsAsync());

            var eventEntity = new EventModel
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                StartTime = eventDto.StartTime,
                EndTime = eventDto.EndTime,
                Recurrence = eventDto.Recurrence == null ? null : new RecurrenceModel
                {
                    RecurrenceType = eventDto.Recurrence.RecurrenceType,
                    RecurrenceCount = eventDto.Recurrence.RecurrenceCount
                }
            };

            var createdEvent = await _eventRepository.AddEventAsync(eventEntity);

            return MapToReadDto(createdEvent);
        }

        public async Task<EventReadDto?> UpdateEventAsync(EventUpdateDto eventDto)
        {
            // Validate update (checks collisions with all events except itself)
            var allEvents = await _eventRepository.GetAllEventsAsync();
            EventEditValidator.EnsureNoCollision(eventDto, allEvents);

            var eventEntity = new EventModel
            {
                EventId = eventDto.EventId,
                Title = eventDto.Title,
                Description = eventDto.Description,
                StartTime = eventDto.StartTime,
                EndTime = eventDto.EndTime,
                Recurrence = eventDto.Recurrence == null ? null : new RecurrenceModel
                {
                    RecurrenceType = eventDto.Recurrence.RecurrenceType,
                    RecurrenceCount = eventDto.Recurrence.RecurrenceCount
                }
            };

            var updatedEvent = await _eventRepository.UpdateEventAsync(eventEntity);

            if (updatedEvent == null) return null;

            return MapToReadDto(updatedEvent);
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            return await _eventRepository.DeleteEventAsync(id);
        }

        // Helper method to map EventModel → EventReadDto
        private EventReadDto MapToReadDto(EventModel e)
        {
            return new EventReadDto
            {
                EventId = e.EventId,
                Title = e.Title,
                Description = e.Description,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Recurrence = e.Recurrence == null ? null : new RecurrenceDto
                {
                    RecurrenceType = e.Recurrence.RecurrenceType,
                    RecurrenceCount = e.Recurrence.RecurrenceCount
                }
            };
        }
    }
}
