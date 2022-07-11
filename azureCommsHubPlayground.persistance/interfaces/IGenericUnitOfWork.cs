using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azureCommsHubPlayground.persistance.interfaces
{
    public interface IGenericUnitOfWork<TPocoEntity> : IDisposable where TPocoEntity : class, IPocoEntity
    {
        public IPocoRepository<TPocoEntity> repository { get; }
        public void Save();
        public Task SaveAsync();
    }
}
