using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Models;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingRoomsController : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingRoomsController(Models.BookingContext context)
        {
            _context = context;
        }

        // GET: api/BookingRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingRoom>>> GetBookingRooms()
        {
            return await _context.BookingRooms
                .Include(b => b.Booking)
                .Include(b => b.Type)
                .ToListAsync();
        }

        // GET: api/BookingRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingRoom>> GetBookingRoom(int id)
        {
            var bookingRoom = await _context.BookingRooms
                .Include(b => b.Booking)
                .Include(b => b.Type)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bookingRoom == null)
            {
                return NotFound();
            }

            return bookingRoom;
        }


        // GET: api/BookingRooms/GetBookingRoomByBooking/5
        [HttpGet("GetBookingRoomByBooking/{id}")]
        public async Task<ActionResult<IEnumerable<BookingRoom>>> GetBookingRoomByBooking(int id)
        {

            var bookingRooms = await _context.BookingRooms
                .Where(br => br.BookingID == id)
                 .Include(br => br.Type)
                .ToListAsync();

            if (bookingRooms == null)
            {
                return NotFound();
            }

            return bookingRooms;
        }

        //  POST: api/BookingRooms
        [HttpPost]
        public async Task<IActionResult> PostBookingRooms([FromBody] List<BookingRoom> bookingRooms)
        {
            if (bookingRooms == null || !bookingRooms.Any())
            {
                return BadRequest("Invalid booking room data.");
            }

            _context.BookingRooms.AddRange(bookingRooms);
            await _context.SaveChangesAsync();

            return Ok(bookingRooms); // Return success response
        }

        // PUT: api/BookingRooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingRoom(int id, [FromBody] BookingRoom bookingRoom)
        {
            if (id != bookingRoom.ID)
            {
                return BadRequest("ID mismatch.");
            }

            _context.Entry(bookingRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingRoomExists(id))
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

        //  DELETE: api/BookingRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingRoom(int id)
        {
            var bookingRoom = await _context.BookingRooms.FindAsync(id);
            if (bookingRoom == null)
            {
                return NotFound();
            }

            _context.BookingRooms.Remove(bookingRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingRoomExists(int id)
        {
            return _context.BookingRooms.Any(e => e.ID == id);
        }
    }
}
