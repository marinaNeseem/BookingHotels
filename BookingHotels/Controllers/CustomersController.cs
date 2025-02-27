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
    public class CustomersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7077/api/Customers/";

        public CustomersController()
        {
            _httpClient = new HttpClient();
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Customer>());
            }

            var customers = JsonConvert.DeserializeObject<List<Customer>>(await response.Content.ReadAsStringAsync());
            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var customer = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NationalNumber,FirstName,LastName,PhoneNumber")] Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiUrl, customer);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error creating customer.");
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var customer = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NationalNumber,FirstName,LastName,PhoneNumber")] Customer customer)
        {
            if (id != customer.Id) return NotFound();

            var response = await _httpClient.PutAsJsonAsync(_apiUrl + id, customer);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error updating customer.");
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var customer = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error deleting customer.");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
