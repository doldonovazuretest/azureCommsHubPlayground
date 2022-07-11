using azureCommsHubPlayground.persistance.unitsOfWork;

namespace azureCommsHubPlayground.backEndService.Services
{
    public class azureBusMessageLogger : IAzureBusMessageLogger, IDisposable
    {
        unitOfWork _unit;
        public azureBusMessageLogger(unitOfWork unit)
        {
            _unit = unit;
        }
        public async Task Log(string processorGuid, string messageGuid, DateTime? enqueued)
        {
            _unit.azureBusMessageEntry.Insert(new azureCommsHubPlayground.persistance.pocoEntities.azureBusMessageEntry()
            {
                guid = messageGuid,
                processorGuid = processorGuid,
                enqueued = enqueued,
                processed = DateTime.Now
            });

            await _unit.SaveAsync();
        }

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unit.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
