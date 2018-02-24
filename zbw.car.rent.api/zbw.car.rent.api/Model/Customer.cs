using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zbw.car.rent.api.Model
{
    public class Customer : IDataObj
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
