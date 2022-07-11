using Azure.Messaging.ServiceBus;

namespace azureCommsHubPlayground.azureHubCommService
{
    public interface IAzureMessageBusSubscriber
    {
        Task register(Func<ProcessMessageEventArgs, Task> processMessage, Func<ProcessErrorEventArgs, Task> processError, Action<string> setGuid);
        Task closeQueueAsync();
        ValueTask disposeAsync();

        string guid { get; }
    }
}
