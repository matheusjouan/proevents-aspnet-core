using ProEvents.Core.Models;

namespace ProEvents.Core.Exceptions;
public abstract class ExceptionBase : Exception
{
    public Error Error { get; set; }

	public ExceptionBase() {}
    public ExceptionBase(string message) : base(message) {}
    public ExceptionBase(string message, Exception innerException) : base(message, innerException) {}
}
