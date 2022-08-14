using MediatR;

namespace Application.Common.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse> { }
