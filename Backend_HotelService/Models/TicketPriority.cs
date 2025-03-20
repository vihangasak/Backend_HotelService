using System.ComponentModel.DataAnnotations;

namespace Backend_HotelService.Models
{
    public class TicketPriority
    {
        [Key]
        public int PriorityId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PriorityName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? SLAHours { get; set; }

        [MaxLength(20)]
        public string? Color { get; set; }
    }
}
