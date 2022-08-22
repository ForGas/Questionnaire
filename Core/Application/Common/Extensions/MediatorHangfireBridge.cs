using MediatR;

namespace Application.Common.Extensions;

public sealed class MediatorHangfireBridge
{
    private readonly IMediator _mediator;

    public MediatorHangfireBridge(IMediator mediator) => _mediator = mediator;

    public async Task Send(IRequest command) => await _mediator.Send(command);

    public async Task Publish(INotification notification) => await _mediator.Publish(notification);
}
