using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;
using DRF.ViewModels;

namespace DRF.Repositories
{
    public interface IRequestStatusRepository : IDapperRepository<RequestStatus>
    {

        IEnumerable<RequestUpdate> GetRequestStatus(int requestId);
    }
    public class RequestStatusRepository : DapperRepository<RequestStatus>, IRequestStatusRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestStatusRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

        public IEnumerable<RequestUpdate> GetRequestStatus(int requestId)
        {
            return Query<RequestUpdate>("SELECT * FROM RequestStatus WHERE RequestId = @RequestId", new { RequestId = requestId }, System.Data.CommandType.Text);
        }

    }
}
