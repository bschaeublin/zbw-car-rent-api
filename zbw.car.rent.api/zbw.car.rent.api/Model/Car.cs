namespace zbw.car.rent.api.Model
{
    public class Car : IDataObj
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int TypeId { get; set;}
        public int ClassId { get; set; }
        public long Kilometers { get; set; }
        public int HorsePower { get; set; }
        public int RegistrationYear { get; set; }
       
    }
}