using azureCommsHubPlayground.azureHubCommService;
using System.Net;

namespace azureCommsHubPlayground.webAdminConsole.Services
{
    public class ipAddressCheckRequestHandlerService : IIpAddressCheckRequestHandlerService
    {
        IAzureMessageBusDispatcher _dispatcher;
        public ipAddressCheckRequestHandlerService(IAzureMessageBusDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        public async Task checkIpAddress(string ipAddress)
        {
            if(IPAddress.TryParse(ipAddress, out IPAddress? _ipAddress))
            {
               await _dispatcher.sendMessage(_ipAddress.GetAddressBytes());
            }
        }
    }
}
