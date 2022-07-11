namespace ipAddressRequestProcessor.Services
{
    public interface IAzureBusMessageLogger : IDisposable
    {
        Task Log(string processorGuid, string messageGuid, DateTime? enqueued);
    }
}
