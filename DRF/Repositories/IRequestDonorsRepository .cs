using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IRequestDonorsRepository : IDapperRepository<LookupsCategory>
    {
        
    }
    public class RequestDonorsRepository : DapperRepository<LookupsCategory>, IRequestDonorsRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestDonorsRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
