using Azure.Messaging.ServiceBus;
using static azureCommsHubPlayground.webAdminConsole.Services.azureIncomingBusMessageEventDispatcher;

namespace azureCommsHubPlayground.webAdminConsole.Services
{
    public interface IAzureIncomingBusMessageEventDispatcher 
    {
        Task processMessage(ProcessMessageEventArgs args);
        Task processError(ProcessErrorEventArgs args);
        void setGuid(string guid);

        string guid { get; }
        event azureBusMessageReceivedEventHandler messageReceived;
    }
}
