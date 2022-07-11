namespace azureCommsHubPlayground.backEndService.Services
{
    public interface IAzureBusMessageLogger : IDisposable
    {
        Task Log(string processorGuid, string messageGuid, DateTime? enqueued);
    }
}
