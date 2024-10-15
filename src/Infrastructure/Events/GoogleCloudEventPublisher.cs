using Application.Events;
using Domain.Exceptions;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using Infrastructure.Configurations;

namespace Infrastructure.Events
{
    public class GoogleCloudEventPublisher : IEventPublisher
    {
        private readonly ILogger<GoogleCloudEventPublisher> _logger;
        private readonly GoogleCloudEventConfig _eventConfig;
        public GoogleCloudEventPublisher(ILogger<GoogleCloudEventPublisher> logger, IOptions<GoogleCloudEventConfig> eventConfig)
        {
            _logger = logger;
            _eventConfig = eventConfig.Value;
        }
        public async Task PublishAsync<T>(T @event) where T : IIntegrationEvent
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };

            if (_eventConfig.EventTopicMap.TryGetValue(@event.EventName, out var topicId))
            {
                var publisher = await PublisherClient.CreateAsync(new TopicName(_eventConfig.ProjectId, topicId));
                var metadata = new Dictionary<string, object?>
            {
                {"hostname", Environment.MachineName},
                {"traceId", Activity.Current?.Id}
            };
                var eventEnvelope = new EventEnvelope<T>(Guid.NewGuid(), @event.EventName, @event, metaData: metadata);
                var eventJson = JsonSerializer.Serialize(eventEnvelope, serializerOptions);
                var pubSubMessage = new PubsubMessage
                {
                    Data = ByteString.CopyFromUtf8(eventJson)
                };
                var response = await publisher.PublishAsync(pubSubMessage);
                _logger.LogInformation("{EventName} event published successfully. Acknowledgment ID: {Response}", @event.EventName, response);
            }
            else
            {
                _logger.LogError("No topic found for event {EventName}", @event.EventName);
                throw new DomainException($"No topic found for event {@event.EventName}", ExceptionCodes.ErrorPublishingEvent.ToString(), 500);
            }
        }
    }
}
