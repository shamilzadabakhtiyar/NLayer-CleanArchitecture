using App.Application.Contracts.ServiceBus;
using MassTransit;

namespace App.Bus
{
    public class ServiceBus(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider) : IServiceBus
    {
        async Task IServiceBus.PublishAsync<T>(T @event, CancellationToken cancellationToken)
        {
            await publishEndpoint.Publish(@event, cancellationToken);
        }

        async Task IServiceBus.SendAsync<T>(T message, string queueName, CancellationToken cancellationToken)
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
            await endpoint.Send(message, cancellationToken);
        }
    }
}
