using DRF.infrastructures;
using DRF.Models;
using DRF.Models.StoredProcedures;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface ILookupsRepository : IDapperRepository<Lookups>
    {
        List<GetByCategory> GetByCategory(LookupsCategoryEnum catId);

    }
    public class LookupsRepository : DapperRepository<Lookups>, ILookupsRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public LookupsRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

        public List<GetByCategory> GetByCategory(LookupsCategoryEnum catId)
        {
            
            return Query<GetByCategory>("SELECT Id as Value, Value as Text, IsActive FROM lookups where CategoryID=@CategoryID", new { CategoryID = (int)catId }, System.Data.CommandType.Text).ToList();
        }
        
    }
}
