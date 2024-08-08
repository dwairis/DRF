using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;
using DRF.ViewModels;

namespace DRF.Repositories
{
    public interface IRequestStatusRepository : IDapperRepository<RequestStatus>
    {

        IEnumerable<RequestStatus> GetRequestStatus(int requestId);
    }
    public class RequestStatusRepository : DapperRepository<RequestStatus>, IRequestStatusRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestStatusRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

        public IEnumerable<RequestStatus> GetRequestStatus(int requestId)
        {
            return Query<RequestStatus>("SELECT * FROM RequestStatus WHERE RequestId = @RequestId", new { RequestId = requestId }, System.Data.CommandType.Text);
        }

    }
}
