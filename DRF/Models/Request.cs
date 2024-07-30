using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRF.Models
{
    public class Request
    {
        public int Id { get; set; }

        public int? ThirdPartyOrganization { get; set; }
        public Organization ThirdPartyOrg { get; set; }

        [Required]
        [StringLength(255)]
        public string ProgramTitle { get; set; }

        public string BriefOnProgram { get; set; }
        public int TotalTarget { get; set; }
        public int TargetRequest { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public string Criteria { get; set; }
        public DateTime? ReferralDeliveryDL { get; set; }
        public int? ReferralTotal { get; set; }

        [StringLength(255)]
        public string CounterPart { get; set; }
        public Organization CounterPartOrg { get; set; }

        [StringLength(255)]
        public string ContactPerson { get; set; }

        [Required]
        [EnumDataType(typeof(RequestStatusEnum))]
        public RequestStatusEnum CurrentStatus { get; set; }

        public string DataFileUrl { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool HiredSelfEmployed { get; set; }
    }

    public enum RequestStatusEnum
    {
        New,
        InProgress,
        Submitted,
        NeedsMoreInfo,
        ApprovedByInitiatedAgency,
        ApprovedByOtherAgency,
        RejectedByInitiatedAgency,
        RejectedByOtherAgency,
        Done
    }
}
