using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BookingHotels.Models;

namespace BookingHotels.Controllers
{
    public class RoomsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7077/api/Rooms/";

        public RoomsController()
        {
            _httpClient = new HttpClient();
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Room>()); // Return an empty list on failure
            }

            var rooms = JsonConvert.DeserializeObject<List<Room>>(await response.Content.ReadAsStringAsync());
            return View(rooms);
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var room = JsonConvert.DeserializeObject<Room>(await response.Content.ReadAsStringAsync());
            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] Room room)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiUrl, room);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error creating room.");
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var room = JsonConvert.DeserializeObject<Room>(await response.Content.ReadAsStringAsync());
            return View(room);
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] Room room)
        {
            if (id != room.Id) return NotFound();

            var response = await _httpClient.PutAsJsonAsync(_apiUrl + id, room);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error updating room.");
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var room = JsonConvert.DeserializeObject<Room>(await response.Content.ReadAsStringAsync());
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error deleting room.");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
