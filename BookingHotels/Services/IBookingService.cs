using BookingHotels.Models;

namespace BookingHotels.Services
{
    public interface IBookingService
    {
        Task<List<Booking>> GetBookings();
        Task<Booking> GetBookingById(int id);
        Task<List<BookingRoom>> GetBookingRoomByBooking(int id);
        Task<Booking> CreateBooking(Booking booking);
        Task<bool> UpdateBooking(Booking booking);
        Task<bool> DeleteBooking(int id);
        Task<List<Customer>> GetCustomers();
        Task<List<HotelBranch>> GetBranches();
        Task<double> GetDiscount(int customerId, int branchId);
    }
}
