using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;
using DRF.ViewModels;

namespace DRF.Repositories
{
    public interface IRequestUpdatesRepository : IDapperRepository<RequestUpdates>
    {

    }
    public class RequestUpdatesRepository : DapperRepository<RequestUpdates>, IRequestUpdatesRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestUpdatesRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }



    }
}
