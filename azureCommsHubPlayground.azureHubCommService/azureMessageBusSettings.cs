using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azureCommsHubPlayground.azureHubCommService
{
    // type providing mappings with applicaiton settings to be used by IServiceCollection.Configure<azureMessageBusSettings> method
    public class azureMessageBusSettings
    {
        public string? dispatcherConnectionString { get; set; }
        public string? dispatcherQueueName { get; set; }
        public string? subscriberConnectionString { get; set; }
        public string? subscriberQueueName { get; set; }
    }
} 
