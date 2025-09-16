using System;
using System.Text.Json.Serialization;
namespace BusinessLogicLayer.DTOs
{
    public class EventUpdateDto
    {
        [JsonPropertyName("eventId")]
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public RecurrenceDto? Recurrence { get; set; }
    }
}