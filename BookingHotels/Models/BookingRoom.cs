 using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookingHotels.Models
{
    public class BookingRoom
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [JsonIgnore]
        [ForeignKey("BookingID")]
        public Booking Booking { get; set; }

        [Required]
        public int TypeID { get; set; }
        [JsonIgnore]
        [ForeignKey("TypeID")]
        public Room Type { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "At least 1 adult is required per room.")]
        public int NumberOfAdults { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Number of children cannot be negative.")]
        public int NumberOfChildren { get; set; }
    }
}
