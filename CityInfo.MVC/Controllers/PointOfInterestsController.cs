using CityInfo.MVC.Data;
using CityInfo.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.MVC.Controllers
{
    [Authorize]
    public class PointOfInterestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PointOfInterestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PointOfInterests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PointsOfInterest.Include(p => p.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PointOfInterests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointOfInterest = await _context.PointsOfInterest
                .Include(p => p.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return View(pointOfInterest);
        }

        // GET: PointOfInterests/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Country");
            return View();
        }

        // POST: PointOfInterests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Description,Latitude,Longitude,CityId")] PointOfInterest pointOfInterest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pointOfInterest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Country", pointOfInterest.CityId);
            return View(pointOfInterest);
        }

        // GET: PointOfInterests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointOfInterest = await _context.PointsOfInterest.FindAsync(id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Country", pointOfInterest.CityId);
            return View(pointOfInterest);
        }

        // POST: PointOfInterests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,Description,Latitude,Longitude,CityId")] PointOfInterest pointOfInterest)
        {
            if (id != pointOfInterest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointOfInterest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointOfInterestExists(pointOfInterest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Country", pointOfInterest.CityId);
            return View(pointOfInterest);
        }

        // GET: PointOfInterests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointOfInterest = await _context.PointsOfInterest
                .Include(p => p.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return View(pointOfInterest);
        }

        // POST: PointOfInterests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pointOfInterest = await _context.PointsOfInterest.FindAsync(id);
            if (pointOfInterest != null)
            {
                _context.PointsOfInterest.Remove(pointOfInterest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointOfInterestExists(int id)
        {
            return _context.PointsOfInterest.Any(e => e.Id == id);
        }
    }
}
