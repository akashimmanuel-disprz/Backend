using DataAccessLayer.Data;           // For AppDbContext
using DataAccessLayer.Interfaces;     // For IEventRepository
using DomainCoreLayer.Entities;       // For EventModel
using Microsoft.EntityFrameworkCore;  // For EF Core async methods
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        // Constructor receives DbContext via dependency injection
        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all events
        public async Task<List<EventModel>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        // Get event by ID
        public async Task<EventModel?> GetEventByIdAsync(int eventId)
        {
            return await _context.Events.FindAsync(eventId);
        }

        // Add a new event
        public async Task<EventModel> AddEventAsync(EventModel newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        // Update an existing event
        public async Task<EventModel?> UpdateEventAsync(EventModel updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(updatedEvent.EventId);
            if (existingEvent == null)
                return null;

            // Update scalar properties
            existingEvent.Title = updatedEvent.Title;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.StartTime = updatedEvent.StartTime;
            existingEvent.EndTime = updatedEvent.EndTime;

            // Update the owned RecurrenceModel safely
            if (updatedEvent.Recurrence != null)
            {
                if (existingEvent.Recurrence == null)
                {
                    existingEvent.Recurrence = new RecurrenceModel();
                }
                existingEvent.Recurrence.RecurrenceType = updatedEvent.Recurrence.RecurrenceType;
                existingEvent.Recurrence.RecurrenceCount = updatedEvent.Recurrence.RecurrenceCount;
            }
            else
            {
                existingEvent.Recurrence = null;
            }

            await _context.SaveChangesAsync();
            return existingEvent;
        }


        // Delete an event by ID
        public async Task<bool> DeleteEventAsync(int eventId)
        {
            var existingEvent = await _context.Events.FindAsync(eventId);
            if (existingEvent == null)
                return false;

            _context.Events.Remove(existingEvent);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
