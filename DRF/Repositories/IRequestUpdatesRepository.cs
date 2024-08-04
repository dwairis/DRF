using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;
using DRF.ViewModels;

namespace DRF.Repositories
{
    public interface IRequestUpdatesRepository : IDapperRepository<RequestUpdates>
    {
        IEnumerable<RequestUpdate> GetRequestUpdates(int requestId);

    }
    public class RequestUpdatesRepository : DapperRepository<RequestUpdates>, IRequestUpdatesRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestUpdatesRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

        public IEnumerable<RequestUpdate> GetRequestUpdates(int requestId)
        {
            // update the query to be @RequestID
            return Query<RequestUpdate>("SELECT * FROM RequestUpdates WHERE RequestId = @RequestId", new { RequestId = requestId }, System.Data.CommandType.Text);
        }


    }
}
