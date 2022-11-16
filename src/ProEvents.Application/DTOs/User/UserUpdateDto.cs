namespace ProEvents.Application.DTOs.User;
public class UserUpdateDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
