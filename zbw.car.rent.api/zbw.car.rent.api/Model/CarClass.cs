namespace zbw.car.rent.api.Model
{
    public class CarClass : IDataObj
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
    }
}