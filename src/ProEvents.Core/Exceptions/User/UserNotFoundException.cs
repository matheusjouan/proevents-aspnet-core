using Microsoft.AspNetCore.Http;
using ProEvents.Core.Models;

namespace ProEvents.Core.Exceptions.User;
public class UserNotFoundException : ExceptionBase
{
	public UserNotFoundException()
	{
		Error = new Error
		{
			Status = StatusCodes.Status404NotFound,
            Message = "This user does not exist"
		};
	}
}
