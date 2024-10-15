using Application.Events;
using Domain.Exceptions;
using Google.Cloud.PubSub.V1;
using Infrastructure.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Events
{
    public class GoogleCloudEventSubscriber : IEventSubscriber
    {
        private readonly ILogger<GoogleCloudEventSubscriber> _logger;
        private readonly GoogleCloudSubscriptionConfig _subscriptionConfig;

        public GoogleCloudEventSubscriber(
            ILogger<GoogleCloudEventSubscriber> logger,
            IOptions<GoogleCloudSubscriptionConfig> subscriptionConfig)
        {
            _subscriptionConfig = subscriptionConfig.Value;
            _logger = logger;
        }

        public Task SubscribeAsync<T>(Func<T, Task> callback, string eventName) where T : IIntegrationEvent
        {
            if (_subscriptionConfig.EventSubscribersMap.TryGetValue(eventName, out var subscriberId))
            {
                var subscriptionName = SubscriptionName.FromProjectSubscription(_subscriptionConfig.ProjectId, subscriberId);
                var subscriber = SubscriberClient.Create(subscriptionName);

                var completionSource = new TaskCompletionSource<T>();

                Task.Run(async () => await subscriber.StartAsync(async (msg, cancellationToken) =>
                {
                    var eventJson = msg.Data.ToStringUtf8();
                    try
                    {
                        var result = JsonSerializer.Deserialize<T>(eventJson, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        await callback.Invoke(result);
                        _logger.LogInformation("Event consumer initialization started");
                        completionSource.SetResult(result);
                        return SubscriberClient.Reply.Ack;
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError("Deserialization error: {Error}", ex.Message);
                        return SubscriberClient.Reply.Nack;
                    }
                }));

                return completionSource.Task;
            }
            else
            {
                _logger.LogError("No subscriber found for event {EventName}", eventName);
                throw new DomainException($"No subscriber found for event {eventName}", ExceptionCodes.ErrorSubscribingToEvent.ToString(), 500);
            }
        }

    }
}
