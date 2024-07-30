using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IRequestPartnersRepository : IDapperRepository<RequestPartners>
    {
        
    }
    public class RequestPartnersRepository : DapperRepository<RequestPartners>, IRequestPartnersRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestPartnersRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
