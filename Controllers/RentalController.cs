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
            return View(_db.Rentals.ToList());
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

            var scooter = _db.Scooters.Single(s => s.Id == rentalViewModel.Scooter.Id);
            var student = _db.Students.Single(s => s.Id == rentalViewModel.Student.Id);

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
    }
}
