using DRF.infrastructures;
using DRF.ViewModels;
using DRF.Models; // Assuming you have a Requests model
using System.Collections.Generic;
using System.Linq;

namespace DRF.Repositories
{
    public interface IRequestsRepository : IDapperRepository<Requests>
    {
        List<RequestViewModel> GetAllRequests();
        RequestDetailViewModel GetRequestDetailsById(int id);
    }

    public class RequestsRepository : DapperRepository<Requests>, IRequestsRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestsRepository(ISqlConnectionsFactory sqlConnectionsFactory)
            : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

        public List<RequestViewModel> GetAllRequests()
        {
            return Query<RequestViewModel>("SELECT Id, ProgramTitle, ProjectStartDate, ProjectEndDate FROM Requests",null,System.Data.CommandType.Text).ToList();
        }

        public RequestDetailViewModel GetRequestDetailsById(int id)
        {
            var requestFromDb = Query<RequestDetailViewModel>("SELECT * FROM Requests WHERE Id = @Id", new { Id = id },System.Data.CommandType.Text).FirstOrDefault();

            if (requestFromDb != null)
            {
                requestFromDb.Partners = Query<string>("SELECT PartnerId FROM RequestPartners WHERE Id = @Id", new { Id = id },System.Data.CommandType.Text).ToList();

                requestFromDb.Donors = Query<string>("SELECT DonorId FROM RequestDonors WHERE Id = @Id", new { Id = id }, System.Data.CommandType.Text).ToList();
            }

            return requestFromDb;
        }
    }
}
