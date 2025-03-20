using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend_HotelService.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }

        [Required]
        [MaxLength(20)]
        public string TicketNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; } = null!;

        public int? RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room? Room { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual TicketStatus Status { get; set; } = null!;

        [Required]
        public int PriorityId { get; set; }

        [ForeignKey("PriorityId")]
        public virtual TicketPriority Priority { get; set; } = null!;

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ClosedAt { get; set; }

        public int? ClosedById { get; set; }

        [ForeignKey("ClosedById")]
        public virtual User? ClosedBy { get; set; }

        [Required]
        public bool IsPublic { get; set; }
    }
}
