using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend_HotelService.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Position { get; set; } = string.Empty;

        [Required]
        public DateOnly HireDate { get; set; }

        public string? EmergencyContact { get; set; }

        public string? EmergencyPhone { get; set; }
    }
}
