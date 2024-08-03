namespace DRF.ViewModels
{
    public class RequestDetailViewModel
    {
        public int RequestId { get; set; }
        public string ProgramTitle { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public List<string> Partners { get; set; }
        public List<string> Donors { get; set; }
        public string Notes { get; set; }
    }
}
