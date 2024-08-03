using DRF.infrastructures;
using DRF.ViewModels;
using DRF.Models; // Assuming you have a Requests model
using System.Collections.Generic;
using System.Linq;

namespace DRF.Repositories
{
    public interface IRequestsRepository : IDapperRepository<Requests>
    {
        IEnumerable<RequestViewModel> GetAllRequests();
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

        public IEnumerable<RequestViewModel> GetAllRequests()
        {
            return Query<RequestViewModel>("SELECT Id as id, ProgramTitle as programTitle, ProjectStartDate as projectStartDate, ProjectEndDate as projectEndDate,ThirdPartyOrganization, CurrentStatus FROM Requests", null,System.Data.CommandType.Text).ToList();
        }


        public RequestDetailViewModel GetRequestDetailsById(int id)
        {
            var requestDetail = Query<RequestDetailViewModel>(
                @"SELECT 
                Id, 
                ThirdPartyOrganization, 
                ProgramTitle, 
                BriefOnProgram, 
                TotalTarget, 
                TargetRequest, 
                ProjectStartDate, 
                ProjectEndDate, 
                Criteria, 
                ReferralDeliveryDL, 
                ReferralTotal, 
                CounterPart, 
                ContactPerson, 
                CurrentStatus, 
                DataFileUrl, 
                CreatedBy, 
                CreatedAt, 
                HiredSelfEmployed 
                FROM Requests 
                WHERE Id = @Id",
                new { Id = id },
                System.Data.CommandType.Text
            ).FirstOrDefault();

            if (requestDetail != null)
            {
                requestDetail.Partners = Query<int>("SELECT PartnerId FROM RequestPartners WHERE RequestId = @Id",new { Id = id },System.Data.CommandType.Text).ToList();

                requestDetail.Donors = Query<int>( "SELECT DonorId FROM RequestDonors WHERE RequestId = @Id", new { Id = id },System.Data.CommandType.Text).ToList();
            }

            return requestDetail;
        }


    }
}
