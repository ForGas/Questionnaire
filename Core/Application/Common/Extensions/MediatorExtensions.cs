using Hangfire;
using MediatR;

namespace Application.Common.Extensions
{
    public static class MediatorExtensions
    {
        public static void Enqueue(this IMediator mediator, IRequest commnad)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangfireBridge>(bridge => bridge.Send(commnad));
        }

        public static void Enqueue(this IMediator mediator, INotification notification)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangfireBridge>(bridge => bridge.Publish(notification));
        }
    }
}
