using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface ILookupsCategoryRepository : IDapperRepository<LookupsCategory>
    {
        
    }
    public class LookupsCategoryRepository : DapperRepository<LookupsCategory>, ILookupsCategoryRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public LookupsCategoryRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
