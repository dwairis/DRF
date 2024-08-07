

using System.ComponentModel.DataAnnotations;

namespace DRF.ViewModels
{
    public class OnCreatePost
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Program Title is required.")]
        [StringLength(100)]
        public string ProgramTitle { get; set; }

        [Required(ErrorMessage = "Donors list is required.")]
        public List<int> Donors { get; set; }

        [Required(ErrorMessage = "Partners list is required.")]
        public List<int> Partners { get; set; }

        public string BriefOnProgram { get; set; }

        public List<int> TargetSectors { get; set; }

        [Required(ErrorMessage = "Target Request is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Target Request must be a positive number.")]
        public int? TargetRequest { get; set; }

        [Required(ErrorMessage = "Total Target is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Total Target must be a positive number.")]
        public int? TotalTarget { get; set; }

        [Required(ErrorMessage = "Referral Delivery Deadline is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ReferralDeliveryDL { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Referral Total must be a positive number.")]
        public int? ReferralTotal { get; set; }

        [StringLength(1000)]
        public string Criteria { get; set; }

        [Required(ErrorMessage = "Project Start Date is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ProjectStartDate { get; set; }

        [Required(ErrorMessage = "Project End Date is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ProjectEndDate { get; set; }

        [StringLength(200)]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Hired/Self-Employed status is required.")]
        public byte? HiredSelfEmployed { get; set; }

        [Required(ErrorMessage = "Counterpart is required.")]
     
        public int? CounterPart { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }
    }
}
