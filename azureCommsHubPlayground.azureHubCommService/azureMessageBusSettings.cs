using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azureCommsHubPlayground.azureHubCommService
{
    public class azureMessageBusSettings
    {
        public string? dispatcherConnectionString { get; set; }
        public string? dispatcherQueueName { get; set; }
        public string? subscriberConnectionString { get; set; }
        public string? subscriberQueueName { get; set; }
        public string? intercommConnectionString { get; set; }
        public string? intercommQueueName { get; set; } 
    }
} 
