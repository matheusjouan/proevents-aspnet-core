using Microsoft.AspNetCore.Http;
using ProEvents.Core.Models;

namespace ProEvents.Core.Exceptions.User;
public class CreateUserException : ExceptionBase
{
	public CreateUserException()
	{
        Error = new Error
        {
            Status = StatusCodes.Status500InternalServerError,
            Message = "Error Creating User"
        };
    }
}
