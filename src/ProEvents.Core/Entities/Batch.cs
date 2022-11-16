namespace ProEvents.Core.Entities;
public class Batch : EntityBase
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Amount { get; set; }

    // EF Relational
    public long EventId { get; set; }
    public Event Event { get; set; }
}
