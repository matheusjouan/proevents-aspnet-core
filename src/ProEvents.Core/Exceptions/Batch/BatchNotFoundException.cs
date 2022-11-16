using Microsoft.AspNetCore.Http;
using ProEvents.Core.Models;

namespace ProEvents.Core.Exceptions.Batch;
public class BatchNotFoundException : ExceptionBase
{
	public BatchNotFoundException()
	{
		Error = new Error
		{
			Status = StatusCodes.Status404NotFound,
			Message = "Batch not found"
		};
	}
}
