using DRF.infrastructures;
using DRF.Models;
using DRF.Utilities;

namespace DRF.Repositories
{
    public interface IUploadedDataRepository : IDapperRepository<UploadedData>
    {
        
    }
    public class UploadedDataRepository : DapperRepository<UploadedData>, IUploadedDataRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public UploadedDataRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

      
    }
}
