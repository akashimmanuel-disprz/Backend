namespace DomainCoreLayer.Entities
{
    public class RecurrenceModel
    {
        public string? RecurrenceType { get; set; } // daily, weekly, monthly, yearly
        public int? RecurrenceCount { get; set; } // -1 = infinite, NULL = one-time
    }

    public class EventModel
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Link recurrence details
        public RecurrenceModel? Recurrence { get; set; }
    }
}