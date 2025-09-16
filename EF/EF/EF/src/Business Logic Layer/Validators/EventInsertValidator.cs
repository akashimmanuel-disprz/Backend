using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.DTOs;
using DomainCoreLayer.Entities;

namespace BusinessLogicLayer.Validators
{
    public static class EventInsertValidator
    {
        public static void EnsureNoCollision(EventCreateDto newEventDto, IEnumerable<EventModel> existingEvents)
        {
            // Map DTO to a temporary EventModel for easier comparison
            var newEvent = new EventModel
            {
                StartTime = newEventDto.StartTime,
                EndTime = newEventDto.EndTime
            };

            foreach (var ev in existingEvents)
            {
                bool isOverlapping = newEvent.StartTime < ev.EndTime && newEvent.EndTime > ev.StartTime;

                if (isOverlapping)
                {
                    throw new ArgumentException(
                        $"Event overlaps with existing event: {ev.Title} ({ev.StartTime} - {ev.EndTime})");
                }
            }
        }
    }
}