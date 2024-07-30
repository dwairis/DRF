using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IOrganizationRepository : IDapperRepository<Organization>
    {
        
    }
    public class OrganizationRepository : DapperRepository<Organization>, IOrganizationRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public OrganizationRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
