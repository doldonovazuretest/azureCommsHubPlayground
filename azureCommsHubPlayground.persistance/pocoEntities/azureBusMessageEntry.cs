using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using azureCommsHubPlayground.persistance.interfaces;

namespace azureCommsHubPlayground.persistance.pocoEntities
{
    public class azureBusMessageEntry : IPocoEntity
    {
        public int Id { get; set; }
        public string? guid { get; set; }
        public DateTime? enqueued { get; set; }
        public DateTime? processed { get; set; }
        public string? processorGuid { get; set; }
        public string? payLoad { get; set; }

        public string enqueuedDateString
        {
            get
            {
                if (enqueued != null) return enqueued.Value.ToString();
                return String.Empty;
            }
        }
        public string processedDateString
        {
            get
            {
                if (processed != null) return processed.Value.ToString();
                return String.Empty;
            }
        }
    }
}
