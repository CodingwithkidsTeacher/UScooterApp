using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UScooter.Models;

namespace UScooter.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Students.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Email,PhoneNumber")] Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Add(student);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/id
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _db.Students.FirstOrDefault(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/id
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Name,Email,PhoneNumber")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (id != student.Id)
                {
                    return NotFound();
                }
                else
                {
                    _db.Update(student);
                    _db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _db.Students.FirstOrDefault(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/id
        [HttpPost]
        public IActionResult Delete(int id, bool notUsed)
        {
            var student = _db.Students.FirstOrDefault(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            _db.Students.Remove(student);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
