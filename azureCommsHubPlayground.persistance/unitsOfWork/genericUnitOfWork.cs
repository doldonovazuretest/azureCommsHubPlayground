using Microsoft.EntityFrameworkCore;
using azureCommsHubPlayground.persistance.database;
using azureCommsHubPlayground.persistance.interfaces;
using azureCommsHubPlayground.persistance.repositories;

namespace azureCommsHubPlayground.persistance.unitsOfWork
{
    /// <summary>
    /// entry point abstraction type that encapsulates database CRUD operations 
    /// </summary>
    /// <typeparam name="TPocoEntity">database entity (table)</typeparam>
    public class genericUnitOfWork<TPocoEntity> : IGenericUnitOfWork<TPocoEntity>, IDisposable where TPocoEntity : class, IPocoEntity
    {
        bool _root;
        private dbContext? context;
        private IPocoRepository<TPocoEntity> _repo;
        public genericUnitOfWork(dbContext Context)
        {
            this.context = Context;
            _root = true;
        }

        public genericUnitOfWork(dbContext Context, bool root)
        {
            this.context = Context;
            _root = root;
        }

        public genericUnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<dbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            this.context = new dbContext(optionsBuilder.Options);
            _root = true;
        }


        public IPocoRepository<TPocoEntity> repository
        {
            get
            {
                if (this._repo == null)
                {
                    this._repo = new entityRepository<TPocoEntity>(context);
                }
                return _repo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public dbContext dbContext => context;

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
