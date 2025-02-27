using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace BookingAPI.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public Int64 NationalNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }

        [JsonIgnore]
        public ICollection<Booking>? Bookings { get; set; }

    }
}
