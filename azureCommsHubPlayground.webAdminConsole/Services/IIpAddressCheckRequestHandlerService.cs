namespace azureCommsHubPlayground.webAdminConsole.Services
{
    public interface IIpAddressCheckRequestHandlerService 
    {
        Task checkIpAddress(string ipAddress);
    }
}
