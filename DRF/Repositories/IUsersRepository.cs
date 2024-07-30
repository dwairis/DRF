using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IUsersRepository : IDapperRepository<LookupsCategory>
    {
        
    }
    public class UsersRepository : DapperRepository<LookupsCategory>, IUsersRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public UsersRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
