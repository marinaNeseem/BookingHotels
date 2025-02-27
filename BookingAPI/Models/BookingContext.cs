using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;
namespace BookingAPI.Models
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingRoom> BookingRooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HotelBranch> HotelBranches { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
