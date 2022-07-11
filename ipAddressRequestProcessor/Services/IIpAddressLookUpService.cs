using ipAddressRequestProcessor.Models;

namespace ipAddressRequestProcessor.Services
{
    public interface IIpAddressLookUpService
    {
        Task<ipAddress> check(string ipAddressData);
    }
}
