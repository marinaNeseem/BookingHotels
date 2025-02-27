using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBranchesController : ControllerBase
    {
        private readonly BookingContext _context;

        public HotelBranchesController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/HotelBranches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelBranch>>> GetHotelBranches()
        {
            return await _context.HotelBranches.ToListAsync();
        }

        // GET: api/HotelBranches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelBranch>> GetHotelBranch(int id)
        {
            var hotelBranch = await _context.HotelBranches.FindAsync(id);

            if (hotelBranch == null)
            {
                return NotFound();
            }

            return hotelBranch;
        }

        // PUT: api/HotelBranches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelBranch(int id, HotelBranch hotelBranch)
        {
            if (id != hotelBranch.ID)
            {
                return BadRequest();
            }

            _context.Entry(hotelBranch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelBranchExists(id))
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

        // POST: api/HotelBranches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelBranch>> PostHotelBranch(HotelBranch hotelBranch)
        {
            _context.HotelBranches.Add(hotelBranch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelBranch", new { id = hotelBranch.ID }, hotelBranch);
        }

        // DELETE: api/HotelBranches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelBranch(int id)
        {
            var hotelBranch = await _context.HotelBranches.FindAsync(id);
            if (hotelBranch == null)
            {
                return NotFound();
            }

            _context.HotelBranches.Remove(hotelBranch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelBranchExists(int id)
        {
            return _context.HotelBranches.Any(e => e.ID == id);
        }
    }
}
