using azureCommsHubPlayground.backEndService.Models;

namespace azureCommsHubPlayground.backEndService.Services
{
    public interface IIpAddressLookUpService
    {
        Task<ipAddress> check(string ipAddressData);
    }
}
