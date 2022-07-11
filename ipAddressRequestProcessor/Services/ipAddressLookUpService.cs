using ipAddressRequestProcessor.Models;
using System.Net;
using System.Text.Json.Nodes;

namespace ipAddressRequestProcessor.Services
{
    public class ipAddressLookUpService : IIpAddressLookUpService
    {
        public async Task<ipAddress> check(string ipAddressData)
        {
            if(IPAddress.TryParse(ipAddressData, out IPAddress? _ipAddress))
            {
                return await pullAndParse(ipAddressData);
            }
            throw new InvalidOperationException("entered data does not represent a valid IP address");
        }

        private async Task<JsonObject> pullIpAddressInfoAsync(string ipAddressData)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string data = await httpClient.GetStringAsync(new Uri("http://ip-api.com/json/" + ipAddressData));

                return JsonNode.Parse(data).AsObject();
            }
        }

        private async Task<ipAddress> pullAndParse(string ipAddressData)
        {
            JsonObject data = await pullIpAddressInfoAsync(ipAddressData);

            return new ipAddress()
            {
                ip = ipAddressData,
                country = (string)data["country"],
                countryCode = (string)data["countryCode"],
                region = (string)data["region"],
                regionName = (string)data["regionName"],
                city = (string)data["city"],
                zip = (string)data["zip"],
                timezone = (string)data["timezone"],
                isp = (string)data["isp"],
                org = (string)data["isp"],
                autonsys = (string)data["as"],
                lattitude = (double)data["lat"],
                longitude = (double)data["lon"]
            };
        }
    }
}
