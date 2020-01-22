using CarService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Data
{
    public class CarServiceContext : DbContext
    {
        public CarServiceContext(DbContextOptions<CarServiceContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(e =>
            {
                e.Property(c => c.Brand).IsRequired();
                e.Property(c => c.Brand).HasMaxLength(30);
                e.Property(c => c.Model).IsRequired();
                e.Property(c => c.Model).HasMaxLength(30);
                e.HasMany(c => c.Bookings)
                    .WithOne(b => b.Car)
                    .HasForeignKey(b => b.CarId);
            });

            modelBuilder.Entity<Booking>(e =>
            {
                e.Property(b => b.BookTime).IsRequired();
                e.Property(b => b.CarId).IsRequired();
                e.Property(b => b.CustomerId).IsRequired();
            });

            modelBuilder.Entity<Customer>(e =>
            {
                e.Property(c => c.FirstName).IsRequired();
                e.Property(c => c.FirstName).HasMaxLength(50);
                e.Property(c => c.LastName).IsRequired();
                e.Property(c => c.LastName).HasMaxLength(50);
                e.HasMany(c => c.Bookings)
                    .WithOne(b => b.Customer)
                    .HasForeignKey(b => b.CustomerId);
            });

        }
    }
}
