namespace DRF.infrastructures
{
    public interface ISqlConnectionsFactory
    {
        string GetMasterDbConnectionString { get; }
       
    }
    public class SqlConnectionsFactory : ISqlConnectionsFactory
    {
        private readonly IConfiguration _configuration;
        public SqlConnectionsFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetMasterDbConnectionString
        {
            get { return _configuration.GetConnectionString("DefaultConnection"); }
        }
     
    }
}
