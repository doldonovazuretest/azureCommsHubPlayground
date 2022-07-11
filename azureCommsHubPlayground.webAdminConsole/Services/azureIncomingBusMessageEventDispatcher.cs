using Azure.Messaging.ServiceBus;

namespace azureCommsHubPlayground.webAdminConsole.Services
{
    public class azureIncomingBusMessageEventDispatcher : IAzureIncomingBusMessageEventDispatcher
    {
        private string _guid = Guid.NewGuid().ToString();

        public delegate void azureBusMessageReceivedEventHandler(object sender, azureBusMessageEventArgs e);

        public event azureBusMessageReceivedEventHandler messageReceived; 
        public event azureBusMessageReceivedEventHandler errorEncountered;

        public async Task processError(ProcessErrorEventArgs args) 
        {
            OnErrorEncountered(new azureBusMessageEventArgs() { payLoad = args.Exception.Message});
        }

        public async Task processMessage(ProcessMessageEventArgs args)
        {
            OnMessageReceived(new azureBusMessageEventArgs() { payLoad = args.Message.Body.ToString() });
            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
        }

        public void setGuid(string guid)
        {
            _guid = guid;
        }

        public string guid => _guid;

        protected virtual void OnMessageReceived(azureBusMessageEventArgs e) 
        {
            messageReceived?.Invoke(this, e);
        }

        protected virtual void OnErrorEncountered(azureBusMessageEventArgs e)
        {
            errorEncountered?.Invoke(this, e);
        }
    }


}
