namespace DRF.ViewModels
{
    public class RequestStatus
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string Update { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
