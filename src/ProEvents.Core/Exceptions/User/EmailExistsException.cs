using Microsoft.AspNetCore.Http;
using ProEvents.Core.Models;

namespace ProEvents.Core.Exceptions.User;
public class EmailExistsException : ExceptionBase
{
	public EmailExistsException()
	{
		Error = new Error
		{
			Status = StatusCodes.Status400BadRequest,
            Message = "Email Already Exists"
        };
	}
}
