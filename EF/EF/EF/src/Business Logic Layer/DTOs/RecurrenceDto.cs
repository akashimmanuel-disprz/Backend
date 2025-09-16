namespace BusinessLogicLayer.DTOs
{
    public class RecurrenceDto
    {
        public string? RecurrenceType { get; set; }   // None, Daily, Weekly...
        public int? RecurrenceCount { get; set; }     // Optional
    }
}
