using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IRequestTargetSectorsRepository : IDapperRepository<LookupsCategory>
    {
        
    }
    public class RequestTargetSectorsRepository : DapperRepository<LookupsCategory>, IRequestTargetSectorsRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestTargetSectorsRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
