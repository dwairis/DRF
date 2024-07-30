using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IRequestsRepository : IDapperRepository<LookupsCategory>
    {
        
    }
    public class RequestsRepository : DapperRepository<LookupsCategory>, IRequestsRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestsRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
