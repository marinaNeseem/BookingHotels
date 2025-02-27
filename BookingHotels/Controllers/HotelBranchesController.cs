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
    public class HotelBranchesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7077/api/HotelBranches/";

        public HotelBranchesController()
        {
            _httpClient = new HttpClient();
        }

        // GET: HotelBranches
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<HotelBranch>()); // Return an empty list on failure
            }

            var hotelBranches = JsonConvert.DeserializeObject<List<HotelBranch>>(await response.Content.ReadAsStringAsync());
            return View(hotelBranches);
        }

        // GET: HotelBranches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var hotelBranch = JsonConvert.DeserializeObject<HotelBranch>(await response.Content.ReadAsStringAsync());
            return View(hotelBranch);
        }

        // GET: HotelBranches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HotelBranches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BranchName")] HotelBranch hotelBranch)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiUrl, hotelBranch);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error creating hotel branch.");
            return View(hotelBranch);
        }

        // GET: HotelBranches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var hotelBranch = JsonConvert.DeserializeObject<HotelBranch>(await response.Content.ReadAsStringAsync());
            return View(hotelBranch);
        }

        // POST: HotelBranches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BranchName")] HotelBranch hotelBranch)
        {
            if (id != hotelBranch.ID) return NotFound();

            var response = await _httpClient.PutAsJsonAsync(_apiUrl + id, hotelBranch);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error updating hotel branch.");
            return View(hotelBranch);
        }

        // GET: HotelBranches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var hotelBranch = JsonConvert.DeserializeObject<HotelBranch>(await response.Content.ReadAsStringAsync());
            return View(hotelBranch);
        }

        // POST: HotelBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error deleting hotel branch.");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
