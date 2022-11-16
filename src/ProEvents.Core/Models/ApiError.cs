using Microsoft.AspNetCore.Http;

namespace ProEvents.Core.Models;
public class ApiError
{
    public Error Error { get; set; }

	public ApiError(string message, int statusCode)
	{
		Error = new Error
		{
			Status = statusCode,
			Message = message,
		};
	}

}
