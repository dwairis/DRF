using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IRequestsRepository : IDapperRepository<Requests>
    {
        
    }
    public class RequestsRepository : DapperRepository<Requests>, IRequestsRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestsRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
