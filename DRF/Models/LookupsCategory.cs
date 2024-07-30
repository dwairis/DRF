using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRF.Models
{
    public class LookupsCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
