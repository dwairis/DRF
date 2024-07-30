using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface ILookupsRepository : IDapperRepository<Lookups>
    {
        List<Lookups> GetByCategory(LookupsCategoryEnum catId);
    }
    public class LookupsRepository : DapperRepository<Lookups>, ILookupsRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public LookupsRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

        public List<Lookups> GetByCategory(LookupsCategoryEnum catId)
        {
            
            return Query<Lookups>("SELECT * FROM lookups where CategoryID=@CategoryID", new { CategoryID = (int)catId }, System.Data.CommandType.Text).ToList();
        }
    }
}
