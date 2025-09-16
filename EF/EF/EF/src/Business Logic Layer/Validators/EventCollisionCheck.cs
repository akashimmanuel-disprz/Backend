using System;
using System.Collections.Generic;
using System.Linq;
using DomainCoreLayer.Entities;

namespace BusinessLogicLayer.Validators
{
    public static class EventCollisionChecker
    {
        public static void EnsureNoCollision(EventModel newEvent, IEnumerable<EventModel> existingEvents)
        {
            foreach (var ev in existingEvents)
            {
                // Skip comparing with itself if EventId matches (for editing)
                if (newEvent.EventId != 0 && newEvent.EventId == ev.EventId)
                    continue;

                // Check for time overlap
                bool isOverlapping = newEvent.StartTime < ev.EndTime && newEvent.EndTime > ev.StartTime;

                if (isOverlapping)
                {
                    throw new ArgumentException($"Event overlaps with existing event: {ev.Title} ({ev.StartTime} - {ev.EndTime})");
                }
            }
        }
    }
}