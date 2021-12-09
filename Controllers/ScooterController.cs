using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UScooter.Models;

namespace UScooter.Controllers
{
    public class ScooterController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ScooterController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Scooters.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Scooter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Color,PhysicalCondition,AvailabilityStatus")] Scooter scooter)
        {
            if (ModelState.IsValid)
            {
                _db.Add(scooter);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(scooter);
        }

        // GET: Scooter/Edit/id
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scooter = _db.Scooters.FirstOrDefault(m => m.Id == id);
            if (scooter == null)
            {
                return NotFound();
            }
            return View(scooter);
        }

        // POST: Scooter/Edit/id
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Name,Color,PhysicalCondition,AvailabilityStatus")] Scooter scooter)
        {
            if (ModelState.IsValid)
            {
                if (id != scooter.Id)
                {
                    return NotFound();
                }
                else
                {
                    _db.Update(scooter);
                    _db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(scooter);
        }

        // GET: Scooter/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scooter = _db.Scooters.FirstOrDefault(m => m.Id == id);
            if (scooter == null)
            {
                return NotFound();
            }

            return View(scooter);
        }

        // POST: Scooter/Delete/id
        [HttpPost]
        public IActionResult Delete(int id, bool notUsed)
        {
            var scooter = _db.Scooters.FirstOrDefault(m => m.Id == id);
            if (scooter == null)
            {
                return NotFound();
            }
            _db.Scooters.Remove(scooter);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
