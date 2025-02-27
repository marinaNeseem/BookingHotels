using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
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
