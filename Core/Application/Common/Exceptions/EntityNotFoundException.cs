namespace Application.Common.Exceptions;

public sealed class EntityNotFoundException : ApplicationException
{
    public EntityNotFoundException() : base() { }

    public EntityNotFoundException(string message) : base(message) { }

    public EntityNotFoundException(string message, Exception ex) : base(message, ex) { }

    public EntityNotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.") { }
}
