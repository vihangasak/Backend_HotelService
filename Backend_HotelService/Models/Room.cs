using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_HotelService.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomType { get; set; }

        [Required]
        public int Floor { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Available";

        [StringLength(255)]
        public string QRCodeUrl { get; set; }
    }
}
