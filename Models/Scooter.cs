using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UScooter.Models
{
    public class Scooter
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the scooter name or number!")]
        [Display(Name = "Scooter Name")]
        public string Name { get; set; }

        public string Color { get; set; }

        [Display(Name = "Physical Condition")]
        public string PhysicalCondition { get; set; }

        [Display(Name = "Availability Status")]
        public string AvailabilityStatus { get; set; }

    }
}
