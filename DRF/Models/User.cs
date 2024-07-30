using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRF.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }

        public int? OrganizationID { get; set; }
        public Organization Organization { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum Role
    {
        SUPER_USER,
        ADMIN,
        AGENCY_USER
    }
}
