using Microsoft.AspNetCore.Http;
using ProEvents.Core.Models;

namespace ProEvents.Core.Exceptions.Event;
public class EventNotExistException : ExceptionBase
{
	public EventNotExistException()
	{
		Error = new Error
		{
			Status = StatusCodes.Status404NotFound,
            Message = "Event not found"
		};
	}
}
