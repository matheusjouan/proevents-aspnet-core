using Microsoft.AspNetCore.Http;
using ProEvents.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEvents.Core.Exceptions.User;
public class InvalidCredentialsException : ExceptionBase
{
	public InvalidCredentialsException()
	{
		Error = new Error
		{
			Message = "Usuário ou senha inválidas",
			Status = StatusCodes.Status401Unauthorized
		};
	}
}
