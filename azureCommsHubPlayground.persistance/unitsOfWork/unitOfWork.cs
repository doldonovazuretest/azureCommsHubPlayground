using Microsoft.EntityFrameworkCore;
using azureCommsHubPlayground.persistance.database;
using azureCommsHubPlayground.persistance.interfaces;
using azureCommsHubPlayground.persistance.repositories;
using azureCommsHubPlayground.persistance.pocoEntities;

namespace azureCommsHubPlayground.persistance.unitsOfWork
{
    public class unitOfWork : IDisposable
    {
        bool _root;

        public unitOfWork(dbContext Context)
        {
            this._context = Context;
        }

        public unitOfWork(dbContext Context, bool root)
        {
            this._context = Context;
            _root = root;
        }

        public unitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<dbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            this._context = new dbContext(optionsBuilder.Options);
            _root = true;
        }

        // properties
        private dbContext _context;

        // private fields 
        #region private fields
        private IPocoRepository<azureBusMessageEntry> _azureBusMessageEntry;
        private IPocoRepository<ipAddress> _ipAddress;       
        #endregion

        // public properties
        #region public properties

        public IPocoRepository<azureBusMessageEntry> azureBusMessageEntry
        {
            get
            {
                if (this._azureBusMessageEntry == null)
                {
                    this._azureBusMessageEntry = new entityRepository<azureBusMessageEntry>(context);
                }
                return _azureBusMessageEntry;
            }
        }
        public IPocoRepository<ipAddress> ipAddress
        {
            get
            {
                if (this._ipAddress == null)
                {
                    this._ipAddress = new entityRepository<ipAddress>(context);
                }
                return _ipAddress;
            }
        }

        #endregion



        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public dbContext context => this._context;


        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_root) context.Dispose();
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
