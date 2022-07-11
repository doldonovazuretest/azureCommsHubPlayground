using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azureCommsHubPlayground.azureHubCommService
{
    public interface IAzureMessageBusDispatcher
    {
        Task sendMessage(byte[] payload);
        Task sendMessage(string message);
    }
}
