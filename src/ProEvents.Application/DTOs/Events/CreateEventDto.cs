namespace ProEvents.Application.DTOs.Events;
public class CreateEventDto
{
    public string Place { get; set; }
    public DateTime? EventDate { get; set; }
    public string Theme { get; set; }
    public int AmountOfPeople { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}
