using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using azureCommsHubPlayground.persistance.pocoEntities;

namespace azureCommsHubPlayground.persistance.database
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options)
           : base(options)
        {
        }

        public DbSet<ipAddress>? ipAddress { get; set; }      
        public DbSet<azureBusMessageEntry>? azureBusMessageEntry { get; set; }

        public static DbContextOptionsBuilder<dbContext> getBuilder(string connectionstring)
        {
            var optionsBuilder = new DbContextOptionsBuilder<dbContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionstring);
            return optionsBuilder;
        }
    }
}
