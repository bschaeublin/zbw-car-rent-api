using System;

namespace zbw.car.rent.api.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int Days { get; set; }
        public ReservationState State { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}