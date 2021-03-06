using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace azureCommsHubPlayground.azureHubCommService
{
    /// <summary>
    /// implementaton of Azure Message Hub Message Dispatcher - sending messages
    /// </summary>   
    public class azureMessageBusDispatcher : IAzureMessageBusDispatcher
    {
        // reference to options of type providing mappings with applicaiton settings
        // will be used in .net core web api and web apps by IServiceCollection.Configure<azureMessageBusSettings> method
        private readonly IOptions<azureMessageBusSettings> _appSettings;
        // the client that owns the connection and can be used to create senders and receivers
        static ServiceBusClient? client;

        // the sender used to publish messages to the queue
        static ServiceBusSender? sender;

        public azureMessageBusDispatcher(IOptions<azureMessageBusSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task sendMessage(byte[] payload)
        {
            client = new ServiceBusClient(_appSettings.Value.dispatcherConnectionString);
            sender = client.CreateSender(_appSettings.Value.dispatcherQueueName);

            try
            {
                await sender.SendMessageAsync(new ServiceBusMessage(new BinaryData(payload)));
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        public async Task sendMessage(string message)
        {
            client = new ServiceBusClient(_appSettings.Value.dispatcherConnectionString);
            sender = client.CreateSender(_appSettings.Value.dispatcherQueueName);

            try
            {
                await sender.SendMessageAsync(new ServiceBusMessage(message));
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
