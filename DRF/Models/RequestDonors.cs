using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRF.Models
{
    public class RequestDonors
    {
        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int DonorId { get; set; }
        public Lookups Donor { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
