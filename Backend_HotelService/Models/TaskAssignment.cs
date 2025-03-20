using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

namespace Backend_HotelService.Models
{
    public class TaskAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }

        [Required]
        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; } = null!;

        public int? AssignedToStaffId { get; set; }

        [ForeignKey("AssignedToStaffId")]
        public virtual Staff? AssignedToStaff { get; set; }

        public int? AssignedToDepartmentId { get; set; }

        [ForeignKey("AssignedToDepartmentId")]
        public virtual Department? AssignedToDepartment { get; set; }

        [Required]
        public int AssignedByUserId { get; set; }

        [ForeignKey("AssignedByUserId")]
        public virtual User AssignedByUser { get; set; } = null!;

        [Required]
        public DateTime AssignedAt { get; set; }

        // Optional Notes
        public string? Notes { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
