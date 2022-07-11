using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using azureCommsHubPlayground.persistance.interfaces;

namespace azureCommsHubPlayground.persistance.pocoEntities
{
    public class ipAddress : IPocoEntity
    {
        public int Id { get; set; }
        public string? guid { get; set; }
        public string? ip { get; set; }
        public string? country { get; set; }
        public string? countryCode { get; set; }
        public string? region { get; set; }
        public string? regionName { get; set; }
        public string? city { get; set; }
        public string? zip { get; set; }
        public double? lattitude { get; set; }
        public double? longitude { get; set; }
        public string? timezone { get; set; }
        public string? isp { get; set; }
        public string? org { get; set; }
        public string? autonsys { get; set; }
        public DateTime? lastUpdate { get; set; }
    }
}
