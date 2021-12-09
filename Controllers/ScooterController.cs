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
    }
}
