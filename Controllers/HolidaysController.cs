using Date_Management_Project.Data;
using Date_Management_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Date_Management_Project.Controllers
{
    [Authorize]
    public class HolidaysController : Controller
    {
        private readonly AppDbContext _context;
        public HolidaysController(AppDbContext context) => _context = context;

        // List all saved holidays (custom + public)
        public async Task<IActionResult> Index()
            => View(await _context.Holidays.ToListAsync());

        // show form to add a custom holiday
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Holiday h)
        {
            if (!ModelState.IsValid)
                return View(h);

            _context.Holidays.Add(h);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var h = await _context.Holidays.FindAsync(id);
            if (h == null) return NotFound();
            return View(h);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Holiday h)
        {
            if (id != h.Id) return NotFound();
            if (!ModelState.IsValid) return View(h);

            _context.Holidays.Update(h);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var h = await _context.Holidays.FindAsync(id);
            if (h == null) return NotFound();
            return View(h);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var h = await _context.Holidays.FindAsync(id);
            _context.Holidays.Remove(h);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
