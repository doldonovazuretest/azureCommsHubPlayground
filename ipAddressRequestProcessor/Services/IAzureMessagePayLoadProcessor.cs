using Azure.Messaging.ServiceBus;

namespace ipAddressRequestProcessor.Services
{
    public interface IAzureMessagePayLoadProcessor
    {
        Task processMessage(ProcessMessageEventArgs args);
        Task processError(ProcessErrorEventArgs args);
        void setGuid(string guid);

        string guid { get; }
    }
}
