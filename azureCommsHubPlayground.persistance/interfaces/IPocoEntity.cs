using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azureCommsHubPlayground.persistance.interfaces
{
    public interface IPocoEntity
    {
        int Id { get; set; }
        string? guid { get; set; }
    }
}
