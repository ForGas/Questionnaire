namespace Application.Common.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException() : this(string.Empty) { }

    public ApplicationException(string message) : base(message) { }

    public ApplicationException(string message, Exception ex) : base(message, ex) { }
}
