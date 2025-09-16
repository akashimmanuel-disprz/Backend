using System;

namespace BusinessLogicLayer.DTOs
{
    public class EventReadDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public RecurrenceDto? Recurrence { get; set; }
    }
}