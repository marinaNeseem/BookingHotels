using BookingHotels.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;

namespace BookingHotels.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;

        public BookingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Booking>> GetBookings()
        {
            return await _httpClient.GetFromJsonAsync<List<Booking>>("https://localhost:7077/api/Bookings");
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Booking>($"https://localhost:7077/api/Bookings/{id}");
        }

        public async Task<List<BookingRoom>> GetBookingRoomByBooking(int id)
        {
            var response = await _httpClient.GetStringAsync($"https://localhost:7077/api/BookingRooms/GetBookingRoomByBooking/{id}");
            var bookingslist = JsonConvert.DeserializeObject<List<BookingRoom>>(response);

            return bookingslist;
        }
        public async Task<Booking> CreateBooking(Booking booking)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7077/api/Bookings", booking);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Booking>() : null;
        }

        public async Task<bool> UpdateBooking(Booking booking)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7077/api/Bookings/{booking.BookingID}", booking);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7077/api/Bookings/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _httpClient.GetFromJsonAsync<List<Customer>>("https://localhost:7077/api/Customers");
        }

        public async Task<List<HotelBranch>> GetBranches()
        {
            return await _httpClient.GetFromJsonAsync<List<HotelBranch>>("https://localhost:7077/api/HotelBranches");
        }

        public async Task<double> GetDiscount(int customerId, int branchId)
        {
            var discountResponse = await _httpClient.GetFromJsonAsync<JsonElement>($"https://localhost:7077/api/Bookings/CheckDiscount?customerId={customerId}&branchId={branchId}");
            return discountResponse.TryGetProperty("discount", out var discount) ? discount.GetDouble() : 0;
        }
    }
}
