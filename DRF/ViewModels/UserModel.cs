namespace DRF.ViewModels
{
    public class UserModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
        public Guid UserId { get; set; }
        public int? ContractorId { get; set; }
        public int[] LocationsArr { get; set; }
        public string Locations { get; set; }
    }
}
