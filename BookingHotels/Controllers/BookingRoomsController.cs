
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using BookingHotels.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BookingHotels.Controllers
{
    public class BookingRoomsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7077/api/BookingRooms/";
        private readonly string _bookingsApiUrl = "https://localhost:7077/api/Bookings/";
        private readonly string _roomsApiUrl = "https://localhost:7077/api/Rooms/";

        public BookingRoomsController()
        {
            _httpClient = new HttpClient();
        }

        // GET: BookingRooms
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<BookingRoom>());
            }

            var bookingRooms = JsonConvert.DeserializeObject<List<BookingRoom>>(await response.Content.ReadAsStringAsync());
            return View(bookingRooms);
        }

        // GET: BookingRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var bookingRoom = JsonConvert.DeserializeObject<BookingRoom>(await response.Content.ReadAsStringAsync());
            return View(bookingRoom);
        }

        // GET: BookingRooms/Create
        public async Task<IActionResult> Create(int BookingID)
        {
            ViewData["BookingID"] = BookingID;

            var roomsResponse = await _httpClient.GetAsync(_roomsApiUrl);
            if (roomsResponse.IsSuccessStatusCode)
            {
                var rooms = JsonConvert.DeserializeObject<List<Room>>(await roomsResponse.Content.ReadAsStringAsync());
                ViewData["TypeID"] = new SelectList(rooms, "Id", "Type");
            }

            return View();
        }

        // POST: BookingRooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int BookingID, int[] RoomTypes, int[] NumberOfAdults, int[] NumberOfChildren)
        {
            if (RoomTypes.Length == 0)
            {
                ModelState.AddModelError("", "At least one room must be added.");
                return View();
            }

            var bookingRooms = new List<BookingRoom>();
            for (int i = 0; i < RoomTypes.Length; i++)
            {
                bookingRooms.Add(new BookingRoom
                {
                    BookingID = BookingID,
                    TypeID = RoomTypes[i],
                    NumberOfAdults = NumberOfAdults[i],
                    NumberOfChildren = NumberOfChildren[i]
                });
            }



            var response = await _httpClient.PostAsJsonAsync(_apiUrl, bookingRooms);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Bookings");
            }

            ModelState.AddModelError("", "Error creating booking room.");
            return View();
        }

        // GET: BookingRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var bookingRoom = JsonConvert.DeserializeObject<BookingRoom>(await response.Content.ReadAsStringAsync());
            return View(bookingRoom);
        }

        // POST: BookingRooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BookingID,TypeID,NumberOfAdults,NumberOfChildren")] BookingRoom bookingRoom)
        {
            if (id != bookingRoom.ID) return NotFound();

            var response = await _httpClient.PutAsJsonAsync(_apiUrl + id, bookingRoom);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error updating booking room.");
            return View(bookingRoom);
        }

        // GET: BookingRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var bookingRoom = JsonConvert.DeserializeObject<BookingRoom>(await response.Content.ReadAsStringAsync());
            return View(bookingRoom);
        }

        // POST: BookingRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error deleting booking room.");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
