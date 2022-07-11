namespace azureCommsHubPlayground.webAdminConsole.Models
{
    public class incomingMesssageLogItemHelper
    {
        public incomingMesssageLogItemHelper()
        {
            guid = Guid.NewGuid().ToString();
        }
        public string? guid { get; set; }
        public string? message { get; set; }
    }
}
