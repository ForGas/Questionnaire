namespace Application.Common.Exceptions;

public sealed class BadRequestException : ApplicationException
{
    public BadRequestException() : base() { }

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(string message, Exception ex) : base(message, ex) { }

    public BadRequestException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was bad request.") { }
}
