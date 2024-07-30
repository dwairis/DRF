using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IRequestTargetSectorsRepository : IDapperRepository<RequestTargetSectors>
    {
        
    }
    public class RequestTargetSectorsRepository : DapperRepository<RequestTargetSectors>, IRequestTargetSectorsRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestTargetSectorsRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
