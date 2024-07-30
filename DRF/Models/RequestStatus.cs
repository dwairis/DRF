using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRF.Models
{
    public class RequestStatus
    {
        public int RequestId { get; set; }
        public Request Request { get; set; }

        [Required]
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Notes { get; set; }
    }

    public enum Status
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
