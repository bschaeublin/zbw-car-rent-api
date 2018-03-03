using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zbw.car.rent.api.Model;

namespace zbw.car.rent.api.Repositories.Database
{
    public class CarRentDbContext : DbContext
    {
        public CarRentDbContext(DbContextOptions<CarRentDbContext> options) : base(options)
        {
            
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }
        public DbSet<CarClass> CarClasses { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Car>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasOne<CarClass>().WithMany();
                c.HasMany<Reservation>().WithOne().HasForeignKey(r => r.CarId).OnDelete(DeleteBehavior.Cascade);
                c.HasMany<RentalContract>().WithOne().HasForeignKey(rc => rc.CarId).OnDelete(DeleteBehavior.Cascade);
                c.HasOne<CarType>().WithMany();
                c.HasOne<CarBrand>().WithMany();
            });

            builder.Entity<Customer>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasMany<Reservation>().WithOne().HasForeignKey(r => r.CustomerId).OnDelete(DeleteBehavior.Restrict);
                c.HasMany<RentalContract>().WithOne().HasForeignKey(rc => rc.CustomerId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<RentalContract>(rc =>
            {
                rc.HasKey(x => x.Id);
                rc.HasOne<Reservation>().WithOne().OnDelete(DeleteBehavior.Cascade);
                rc.HasOne<Customer>().WithMany().OnDelete(DeleteBehavior.SetNull).IsRequired(false);
                rc.HasOne<Car>().WithMany().OnDelete(DeleteBehavior.SetNull).IsRequired(false);
            });

            builder.Entity<Reservation>(rc =>
            {
                rc.HasKey(x => x.Id);
                rc.HasOne<Customer>().WithMany().OnDelete(DeleteBehavior.SetNull).IsRequired(false);
                rc.HasOne<Car>().WithMany().OnDelete(DeleteBehavior.SetNull).IsRequired(false);
            });
        }
    }
}
