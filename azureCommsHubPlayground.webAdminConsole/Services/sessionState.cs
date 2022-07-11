namespace azureCommsHubPlayground.webAdminConsole.Services
{
    public class sessionState : ISessionState
    {
        private Guid _guid;
        public sessionState()
        {
            _guid = Guid.NewGuid();
        }

        public string guid => _guid.ToString();
    }
}
