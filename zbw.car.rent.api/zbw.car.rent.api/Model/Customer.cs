using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zbw.car.rent.api.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }

    public class Reservation
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int Days { get; set; }
        public ReservationState State { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReservationDate { get; set; }
    }

    public class RentalContract
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int Days { get; set; }
        public decimal TotalCosts { get; set; }
        public DateTime RentalDate { get; set; }
    }

    public enum ReservationState
    {
        Pending = 1,
        Reserved = 2,
        Contracted = 3
    }

    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public CarType Type { get; set;}
        public CarClass Class { get; set; }
        
    }

    public class CarType
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class CarBrand
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class CarClass
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
    }
}
