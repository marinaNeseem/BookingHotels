 using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookingAPI.Models
{
    public class BookingRoom
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int BookingID { get; set; }
        [ForeignKey("BookingID")]
        public Booking? Booking { get; set; }

        [Required]
        public int TypeID { get; set; }

        [ForeignKey("TypeID")]
        public Room? Type { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Adults Should be between (1,10).")]
        public int NumberOfAdults { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Children Should be between (1,10).")]
        public int NumberOfChildren { get; set; }
    }
}
