using DRF.infrastructures;
using DRF.Models.StoredProcedures;
using DRF.ViewModels;
namespace DRF.Repositories
{
    public interface IRequestsRepository : IDapperRepository<Requests>
    {
        IEnumerable<RequestViewModel> GetAllRequests();
        RequestDetailViewModel GetRequestDetailsById(int id);
        bool UpdateRequest(RequestDetailViewModel updateModel);
        GetRequestDetails GetRequestDetails(int id);
        int DeleteRequestMultiValues(int id);
    }

    public class RequestsRepository : DapperRepository<Requests>, IRequestsRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public RequestsRepository(ISqlConnectionsFactory sqlConnectionsFactory)
            : base(sqlConnectionsFactory.GetMasterDbConnectionString) // No parentheses
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

        public int DeleteRequestMultiValues(int id)
        {
            return Execute("DeleteRequestMultiValues", new { @Id = id }, System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<RequestViewModel> GetAllRequests()
        {
            return Query<RequestViewModel>(
                "SELECT Id as id, ProgramTitle as programTitle, ProjectStartDate as projectStartDate, ProjectEndDate as projectEndDate,ThirdPartyOrganization, CurrentStatus FROM Requests",
                null,
                System.Data.CommandType.Text
            ).ToList();
        }

        public GetRequestDetails GetRequestDetails(int id)
        {
            return QueryFirstOrDefult<GetRequestDetails>("GetRequestDetails", new { Id = id }, System.Data.CommandType.StoredProcedure);
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
                requestDetail.Partners = Query<int>(
                    "SELECT PartnerId FROM RequestPartners WHERE RequestId = @Id",
                    new { Id = id },
                    System.Data.CommandType.Text
                ).ToList();

                requestDetail.Donors = Query<int>(
                    "SELECT DonorId FROM RequestDonors WHERE RequestId = @Id",
                    new { Id = id },
                    System.Data.CommandType.Text
                ).ToList();
            }

            return requestDetail;
        }

        public bool UpdateRequest(RequestDetailViewModel updateModel)
        {
            try
            {
                Console.WriteLine("Attempting to update request with ID: " + updateModel.Id);

                var sql = @"
            UPDATE Requests
            SET
                ThirdPartyOrganization = @ThirdPartyOrganization,
                ProgramTitle = @ProgramTitle,
                BriefOnProgram = @BriefOnProgram,
                TotalTarget = @TotalTarget,
                TargetRequest = @TargetRequest,
                ProjectStartDate = @ProjectStartDate,
                ProjectEndDate = @ProjectEndDate,
                Criteria = @Criteria,
                ReferralDeliveryDL = @ReferralDeliveryDL,
                ReferralTotal = @ReferralTotal,
                CounterPart = @CounterPart,
                ContactPerson = @ContactPerson,
                CurrentStatus = @CurrentStatus,
                DataFileUrl = @DataFileUrl,
                CreatedBy = @CreatedBy,
                CreatedAt = @CreatedAt,
                HiredSelfEmployed = @HiredSelfEmployed
            WHERE Id = @Id";

                var affectedRows = Query<int>(sql, new
                {
                    updateModel.Id,
                    updateModel.ThirdPartyOrganization,
                    updateModel.ProgramTitle,
                    updateModel.BriefOnProgram,
                    updateModel.TotalTarget,
                    updateModel.TargetRequest,
                    updateModel.ProjectStartDate,
                    updateModel.ProjectEndDate,
                    updateModel.Criteria,
                    updateModel.ReferralDeliveryDL,
                    updateModel.ReferralTotal,
                    updateModel.CounterPart,
                    updateModel.ContactPerson,
                    updateModel.CurrentStatus,
                    updateModel.DataFileUrl,
                    updateModel.CreatedBy,
                    updateModel.CreatedAt,
                    updateModel.HiredSelfEmployed
                }, System.Data.CommandType.Text).FirstOrDefault();

                Console.WriteLine("Rows affected: " + affectedRows);

                if (updateModel.Partners != null)
                {
                    // Delete existing partners
                    Query<int>(
                        "DELETE FROM RequestPartners WHERE RequestId = @Id",
                        new { Id = updateModel.Id },
                        System.Data.CommandType.Text
                    );

                    // Insert updated partners
                    foreach (var partnerId in updateModel.Partners)
                    {
                        Query<int>(
                            "INSERT INTO RequestPartners (RequestId, PartnerId) VALUES (@RequestId, @PartnerId)",
                            new { RequestId = updateModel.Id, PartnerId = partnerId },
                            System.Data.CommandType.Text
                        );
                    }
                }

                if (updateModel.Donors != null)
                {
                    // Delete existing donors
                    Query<int>(
                        "DELETE FROM RequestDonors WHERE RequestId = @Id",
                        new { Id = updateModel.Id },
                        System.Data.CommandType.Text
                    );

                    // Insert updated donors
                    foreach (var donorId in updateModel.Donors)
                    {
                        Query<int>(
                            "INSERT INTO RequestDonors (RequestId, DonorId) VALUES (@RequestId, @DonorId)",
                            new { RequestId = updateModel.Id, DonorId = donorId },
                            System.Data.CommandType.Text
                        );
                    }
                }

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during update: " + ex.Message);
                // Add more logging or error handling as needed
                throw new Exception("Failed to update request: " + ex.Message);
            }
        }




    }
}
