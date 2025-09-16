using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.DTOs;
using DomainCoreLayer.Entities;

namespace BusinessLogicLayer.Validators
{
    public static class EventEditValidator
    {
        public static void EnsureNoCollision(EventUpdateDto eventDto, IEnumerable<EventModel> existingEvents)
        {
            // Map DTO → domain model inside validator
            var newEvent = new EventModel
            {
                EventId = eventDto.EventId,
                Title = eventDto.Title,
                StartTime = eventDto.StartTime,
                EndTime = eventDto.EndTime
            };

            foreach (var ev in existingEvents)
            {
                // Skip comparing with itself
                if (newEvent.EventId != 0 && newEvent.EventId == ev.EventId)
                    continue;

                bool isOverlapping = newEvent.StartTime < ev.EndTime && newEvent.EndTime > ev.StartTime;

                if (isOverlapping)
                    throw new ArgumentException($"Event overlaps with existing event: {ev.Title} ({ev.StartTime} - {ev.EndTime})");
            }
        }
    }
}