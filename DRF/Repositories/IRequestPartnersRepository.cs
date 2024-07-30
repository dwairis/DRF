using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IRequestPartnersRepository : IDapperRepository<LookupsCategory>
    {
        
    }
    public class RequestPartnersRepository : DapperRepository<LookupsCategory>, IRequestPartnersRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestPartnersRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
