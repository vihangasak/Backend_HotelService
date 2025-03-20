using System.ComponentModel.DataAnnotations;

namespace Backend_HotelService.Models
{
    public class TicketStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(20)]
        public string? Color { get; set; }
    }
}
