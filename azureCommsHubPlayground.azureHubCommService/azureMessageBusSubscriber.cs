using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace azureCommsHubPlayground.azureHubCommService
{
    /// <summary>
    /// implementaton of Azure Message Hub Message Subscriber - will sign-up to receive messages
    /// </summary>   
    public class azureMessageBusSubscriber : IAzureMessageBusSubscriber
    {
        private readonly IOptions<azureMessageBusSettings> _appSettings;
        private readonly ServiceBusClient _client;
        private ServiceBusProcessor _processor;
        private readonly Guid _guid;

        public azureMessageBusSubscriber(IOptions<azureMessageBusSettings> appSettings)
        {
            _appSettings = appSettings;
            _guid = Guid.NewGuid();
            _client = new ServiceBusClient(_appSettings.Value.subscriberConnectionString);
        }

        // this will be used in communication to identify message processor (useful when multiple instances are runnging).
        public string guid => _guid.ToString();

        public async Task closeQueueAsync()
        {
            await _processor.CloseAsync().ConfigureAwait(false);
        }

        public async ValueTask disposeAsync()
        {
            if (_processor != null)
            {
                await _processor.DisposeAsync().ConfigureAwait(false);
            }

            if (_client != null)
            {
                await _client.DisposeAsync().ConfigureAwait(false);
            }
        }

        public async Task register(Func<ProcessMessageEventArgs, Task> processMessage, Func<ProcessErrorEventArgs, Task> processError, Action<string> setGuid)
        {
            ServiceBusProcessorOptions _serviceBusProcessorOptions = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false,
            };

            setGuid(guid);
            _processor = _client.CreateProcessor(_appSettings.Value.subscriberQueueName, _serviceBusProcessorOptions);
            _processor.ProcessMessageAsync += processMessage;
            _processor.ProcessErrorAsync += processError;
            await _processor.StartProcessingAsync().ConfigureAwait(false);
        }
    }
}
