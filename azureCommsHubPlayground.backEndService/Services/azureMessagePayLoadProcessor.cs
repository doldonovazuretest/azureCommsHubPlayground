using azureCommsHubPlayground.azureHubCommService;
using Azure.Messaging.ServiceBus;
using System.Net;

namespace azureCommsHubPlayground.backEndService.Services
{
    public class azureMessagePayLoadProcessor : IAzureMessagePayLoadProcessor
    {
        private IAzureMessageBusDispatcher _reportDispatcher;
        private IAzureBusMessageLogger _logger;
        private string _guid = Guid.NewGuid().ToString();
        int messageCount = 0;
        public azureMessagePayLoadProcessor(IAzureMessageBusDispatcher reportDispatcher, IAzureBusMessageLogger logger)
        {
            _reportDispatcher = reportDispatcher;
            _logger = logger;
        }
        public async Task processError(ProcessErrorEventArgs args)
        {
            await _reportDispatcher
               .sendMessage($"processor {this._guid} has run into an issue: {args.Exception.Message}")
               .ConfigureAwait(false);
        }

        public async Task processMessage(ProcessMessageEventArgs args)
        {
            IPAddress _ip = new IPAddress(args.Message.Body.ToArray());

            await _reportDispatcher
                .sendMessage($"{messageCount} - {args.Message.MessageId} processed by {guid} for {_ip.ToString()} at {DateTime.Now}")
                .ConfigureAwait(false);

            await _logger.Log(guid, args.Message.MessageId, args.Message.EnqueuedTime.UtcDateTime).ConfigureAwait(false);

            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);

            messageCount++;
        }

        public void setGuid(string guid)
        {
            _guid = guid;
        }

        public string guid => _guid;
    }
}
