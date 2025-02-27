using System.ComponentModel.DataAnnotations;

namespace BookingHotels.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
