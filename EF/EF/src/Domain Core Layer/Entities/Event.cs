namespace EF.Domain_Core_Layer.Entities;

public class Event
{
    public int EventId { get; set; }      
    public string Title { get; set; }     
    public string Description { get; set; } 
    public DateTime StartTime { get; set; } 
    public DateTime EndTime { get; set; }   
}