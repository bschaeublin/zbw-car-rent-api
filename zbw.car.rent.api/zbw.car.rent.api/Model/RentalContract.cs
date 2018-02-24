using System;

namespace zbw.car.rent.api.Model
{
    public class RentalContract : IDataObj
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int Days { get; set; }
        public decimal TotalCosts { get; set; }
        public DateTime RentalDate { get; set; }
        public int ReservationId { get; set; }
    }
}