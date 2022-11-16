using Microsoft.AspNetCore.Http;
using ProEvents.Core.Models;

namespace ProEvents.Core.Exceptions;
public class TransactionException : ExceptionBase
{
	public TransactionException()
	{
		Error = new Error
		{
			Status = StatusCodes.Status500InternalServerError,
			Message = "An error occurred in the transaction"
		};
	}
}
