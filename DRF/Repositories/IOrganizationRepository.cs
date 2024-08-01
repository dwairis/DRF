using DRF.infrastructures;
using DRF.Models;
using DRF.Models.StoredProcedures;
using DRF.Utilities;
using System.Collections.Generic;

namespace DRF.Repositories
{
    public interface IOrganizationRepository : IDapperRepository<Organization>
    {
        List<GetOrganizationAsDropDown> GetOrganizationAsDropDown();
        List<GetOrganizationAsDropDown> GetCounterPartOrganization();
    }
    public class OrganizationRepository : DapperRepository<Organization>, IOrganizationRepository
    {
        private readonly ISqlConnectionsFactory sqlConnectionsFactory;

        public OrganizationRepository(ISqlConnectionsFactory sqlConnectionsFactory) : base(sqlConnectionsFactory.GetMasterDbConnectionString)
        {
            this.sqlConnectionsFactory = sqlConnectionsFactory;
        }

        public List<GetOrganizationAsDropDown> GetOrganizationAsDropDown()
        {
            return Query<GetOrganizationAsDropDown>("SELECT[Id] as Value,[Name] as Text,[IsActive] FROM [dbo].[Organization]", null, System.Data.CommandType.Text).ToList();
        }
        public List<GetOrganizationAsDropDown> GetCounterPartOrganization()
        {

            return Query<GetOrganizationAsDropDown>("SELECT[Id] as Value,[Name] as Text,[IsActive] FROM [dbo].[Organization] where Type=11", null, System.Data.CommandType.Text).ToList();
        }
    }


}
