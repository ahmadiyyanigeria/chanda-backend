namespace Application.Events
{
    public interface IEventSubscriber
    {
        Task SubscribeAsync<T>(Func<T, Task> eventFactory, string eventName) where T : IIntegrationEvent;
    }
}
