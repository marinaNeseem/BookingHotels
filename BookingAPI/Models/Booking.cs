using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookingAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        [DisplayName("Customer")]
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }

        [Required]
        [DisplayName("Branch")]
        public int BranchID { get; set; }

        [ForeignKey("BranchID")]
        public HotelBranch? Branch { get; set; }

        [Required]
        [DisplayName("Check In Date")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DisplayName("Check Out Date")]
        public DateTime CheckOutDate { get; set; }


        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Discount { get; set; } = 0;

        public ICollection<Room>? Rooms { get; set; }
    }
}
