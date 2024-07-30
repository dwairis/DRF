using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRF.Models
{
    public class RequestPartners
    {
        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int PartnerId { get; set; }
        public Lookups Partner { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
