

using System.ComponentModel.DataAnnotations;

namespace DRF.ViewModels
{
    public class OnCreatePost
    {
        [Required(AllowEmptyStrings = false,ErrorMessage ="")]
        public string ProgramTitle { get; set; }
        public List<int> Donors { get; set; }
        public List<int> Partners { get; set; }
        public string BriefOnProgram { get; set; }
        public List<int> TargetSectors { get; set; }
        public string TargetRequest { get; set; }
        public string TotalTarget { get; set; }
        public string ReferralDeliveryDL { get; set; }
        public string ReferralTotal { get; set; }
        public string Criteria { get; set; }
        public string ProjectStartDate { get; set; }
        public string ProjectEndDate { get; set; }
        public string ContactPerson { get; set; }
        public string HiredSelfEmployed { get; set; }
        public string CounterPart { get; set; }
        public string Notes { get; set; }
    }
}
