using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IRequestUpdatesRepository : IDapperRepository<LookupsCategory>
    {
        
    }
    public class RequestUpdatesRepository : DapperRepository<LookupsCategory>, IRequestUpdatesRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestUpdatesRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
