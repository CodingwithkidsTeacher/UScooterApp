using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UScooter.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Scooter> Scooters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Rental> Rentals { get; set; }

    }
}
