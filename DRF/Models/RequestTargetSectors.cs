using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRF.Models
{
    public class RequestTargetSectors
    {
        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int TargetSectorsID { get; set; }
        public Lookups TargetSector { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
