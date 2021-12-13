using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UScooter.Models;
using UScooter.ViewModels;

namespace UScooter.Controllers
{
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RentalController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var rentals = _db.Rentals.Include(r => r.Student).Include(s => s.Scooter).ToList();
            return View(rentals);
        }

        public IActionResult Create()
        {
            var rentalViewModel = new RentalViewModel
            {
                AvailableScooters = _db.Scooters.Where(s => s.AvailabilityStatus == "Available"),
                AllStudents = _db.Students.ToList()
            };

            return View(rentalViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RentalViewModel rentalViewModel)
        {
            if (!ModelState.IsValid)
            {
                rentalViewModel.AvailableScooters = _db.Scooters.Where(s => s.AvailabilityStatus == "Available");
                rentalViewModel.AllStudents = _db.Students.ToList();
                
                return View(rentalViewModel);
            }

            var student = _db.Students.FirstOrDefault(m => m.Id == rentalViewModel.StudentId);
            var scooter = _db.Scooters.FirstOrDefault(m => m.Id == rentalViewModel.ScooterId);

            var rental = new Rental
            {
                Scooter = scooter,
                Student = student,
                DateRented = rentalViewModel.DateRented,
                ReturnDate = rentalViewModel.ReturnDate
            };

            _db.Add(rental);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Rental/Edit/id
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = _db.Rentals.Include(s => s.Student)
                                    .Include(s => s.Scooter)
                                    .FirstOrDefault(r => r.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            var rentalViewModel = new RentalViewModel
            {
                Student = rental.Student,
                Scooter = rental.Scooter,
                AvailableScooters = _db.Scooters.Where(s => s.AvailabilityStatus == "Available"),
                AllStudents = _db.Students.ToList()
            };

            return View(rentalViewModel);
        }

        // POST: Rental/Edit/id
        [HttpPost]
        public IActionResult Edit(RentalViewModel rentalViewModel)
        {
            if (!ModelState.IsValid)
            {
                rentalViewModel.AvailableScooters = _db.Scooters.Where(s => s.AvailabilityStatus == "Available");
                rentalViewModel.AllStudents = _db.Students.ToList();

                return View(rentalViewModel);
            }

            var student = _db.Students.FirstOrDefault(m => m.Id == rentalViewModel.StudentId);
            var scooter = _db.Scooters.FirstOrDefault(m => m.Id == rentalViewModel.ScooterId);

            //Update
            var rental = _db.Rentals.FirstOrDefault(r => r.Id == rentalViewModel.Id);

            rental.Student = student;
            rental.Scooter = scooter;
            rental.DateRented = rentalViewModel.DateRented;
            rental.ReturnDate = rentalViewModel.ReturnDate;

            _db.Update(rental);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        // GET: Rental/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = _db.Rentals.Include(s => s.Student)
                                    .Include(s=> s.Scooter)
                                    .FirstOrDefault(r => r.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rental/Delete/id
        [HttpPost]
        public IActionResult Delete(int id, bool notUsed)
        {
            var rental = _db.Rentals.FirstOrDefault(r => r.Id == id);
            if (rental == null)
            {
                return NotFound();
            }
            _db.Rentals.Remove(rental);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
