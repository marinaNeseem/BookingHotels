using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;
using BookingAPI.Models;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingsController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        {
            return await _context.Booking
                    .Include(b => b.Customer)
                    .Include(b=>b.Branch)
                    .ToListAsync();

        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Booking
                .Include(b => b.Branch)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingID)
            {
                return BadRequest("Booking ID mismatch.");
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            // Validate customer and branch existence
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == booking.CustomerID);
            var branchExists = await _context.HotelBranches.AnyAsync(b => b.ID == booking.BranchID);

            if (!customerExists || !branchExists)
            {
                return BadRequest("Invalid CustomerID or BranchID.");
            }

            // Check if the customer has a previous booking
            bool hasPreviousBooking = await _context.Booking
                .AnyAsync(b => b.CustomerID == booking.CustomerID);

            // Apply 5% discount if applicable
            if (hasPreviousBooking)
            {
                booking.Discount = 5.00m;
            }

            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.BookingID }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Bookings/CheckDiscount?customerId=1&branchId=2
        [HttpGet("CheckDiscount")]
        public async Task<ActionResult<object>> CheckDiscount(int customerId, int branchId)
        {
            bool hasPreviousBooking = await _context.Booking.AnyAsync(b => b.CustomerID == customerId && b.BranchID == branchId);
            return new { discount = hasPreviousBooking ? 5.00m : 0.00m };
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingID == id);
        }
    }
}
