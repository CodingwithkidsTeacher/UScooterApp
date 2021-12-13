using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UScooter.Models;

namespace UScooter.ViewModels
{
    public class RentalViewModel
    {
        public int Id { get; set; }

        public int ScooterId { get; set; }
        public int StudentId { get; set; }

        public Student Student { get; set; }
        public Scooter Scooter { get; set; }

        public DateTime DateRented { get; set; }
        public DateTime ReturnDate { get; set; }

        public IEnumerable<Scooter> AvailableScooters { get; set; }
        public IEnumerable<Student> AllStudents { get; set; }
    }
}
