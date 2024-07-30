using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IRequestStatusRepository : IDapperRepository<LookupsCategory>
    {
        
    }
    public class RequestStatusRepository : DapperRepository<LookupsCategory>, IRequestStatusRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestStatusRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
