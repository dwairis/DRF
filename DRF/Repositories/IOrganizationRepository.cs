using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IOrganizationRepository : IDapperRepository<LookupsCategory>
    {
        
    }
    public class OrganizationRepository : DapperRepository<LookupsCategory>, IOrganizationRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public OrganizationRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
