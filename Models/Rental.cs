using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UScooter.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Scooter Scooter { get; set; }

        [Required]
        public Student Student { get; set; }

        [Display(Name = "Start Date/Time")]
        public DateTime DateRented { get; set; }

        [Display(Name = "Return Date/Time")]
        public DateTime ReturnDate { get; set; }
    }
}
