using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRF.Models
{
    public class UploadedData
    {
        public int Id { get; set; }

        public int? RequestId { get; set; }
        public Request Request { get; set; }

        [Required]
        [StringLength(255)]
        public string FileUrl { get; set; }

        [Required]
        [StringLength(255)]
        public string CaseNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string IndividualId { get; set; }

        public int RoundNumber { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
