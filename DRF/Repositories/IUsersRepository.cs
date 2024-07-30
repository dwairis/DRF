using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IUsersRepository : IDapperRepository<Users>
    {
        
    }
    public class UsersRepository : DapperRepository<Users>, IUsersRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public UsersRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
