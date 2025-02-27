using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingHotels.Models;
using BookingHotels.Services;

namespace BookingHotels.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetBookings();
            return View(bookings);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Retrieve all BookingRooms related to this BookingID
            var bookingRooms = await _bookingService.GetBookingRoomByBooking(id);
            // Pass the bookingRooms to the view
            ViewData["BookingRooms"] = bookingRooms;
            return View(booking);
        }

        // GET: Bookings/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BranchID"] = new SelectList(await _bookingService.GetBranches(), "ID", "BranchName");
            ViewData["CustomerID"] = new SelectList(await _bookingService.GetCustomers(), "Id", "FirstName");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                var createdBooking = await _bookingService.CreateBooking(booking);
                if (createdBooking != null)
                {
                    return RedirectToAction("Create", "BookingRooms", new { BookingID = createdBooking.BookingID });
                }
                ModelState.AddModelError("", "Failed to create booking.");
            }

            ViewData["BranchID"] = new SelectList(await _bookingService.GetBranches(), "ID", "BranchName", booking.BranchID);
            ViewData["CustomerID"] = new SelectList(await _bookingService.GetCustomers(), "Id", "FirstName", booking.CustomerID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["BranchID"] = new SelectList(await _bookingService.GetBranches(), "ID", "BranchName", booking.BranchID);
            ViewData["CustomerID"] = new SelectList(await _bookingService.GetCustomers(), "Id", "FirstName", booking.CustomerID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.BookingID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var updated = await _bookingService.UpdateBooking(booking);
                if (updated)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update booking.");
            }

            ViewData["BranchID"] = new SelectList(await _bookingService.GetBranches(), "ID", "BranchName", booking.BranchID);
            ViewData["CustomerID"] = new SelectList(await _bookingService.GetCustomers(), "Id", "FirstName", booking.CustomerID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _bookingService.DeleteBooking(id);
            if (!deleted)
            {
                ModelState.AddModelError("", "Failed to delete booking.");
                return View(await _bookingService.GetBookingById(id));
            }
            return RedirectToAction(nameof(Index));
        }

        // AJAX API to check discount
        [HttpGet]
        public async Task<JsonResult> CheckDiscount(int customerId, int branchId)
        {
            var discount = await _bookingService.GetDiscount(customerId, branchId);
            return Json(new { discount });
        }
    }
}
