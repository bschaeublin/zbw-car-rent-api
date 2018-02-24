namespace zbw.car.rent.api.Model
{
    public class Car : IDataObj
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public CarType Type { get; set;}
        public CarClass Class { get; set; }
       
    }
}