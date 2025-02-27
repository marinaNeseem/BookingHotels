using System.ComponentModel.DataAnnotations;

namespace BookingHotels.Models
{
    public class HotelBranch
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public required string BranchName { get; set; }

    }
}
