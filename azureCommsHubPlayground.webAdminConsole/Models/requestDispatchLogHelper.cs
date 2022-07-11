namespace azureCommsHubPlayground.webAdminConsole.Models
{
    public class requestDispatchLogHelper
    {
        public string ip { get; set; }
        public DateTime? time { get; set; }

        public string? timeStamp
        {
            get
            {
                if (time != null) return time.ToString();
                return string.Empty;
            }
        }
    }
}
