using System;
using System.Linq;
using System.Threading.Tasks;
using zbw.car.rent.api.Model;
using zbw.car.rent.api.Repositories;

namespace zbw.car.rent.api.Demo
{
    public class DemoInitializer
    {
        public IRepository<CarClass> ClassRepository { get; }
        public IRepository<CarBrand> BrandRepository { get; }
        public IRepository<CarType> TypeRepository { get; }
        public IRepository<Car> CarRepository { get; }
        public IRepository<Customer> CustomerRepository { get; }
        public IRepository<Reservation> ReservationRepository { get; }
        public IRepository<RentalContract> ContractRepository { get; }

        public DemoInitializer(
            IRepository<CarClass> classRepository,
            IRepository<CarBrand> brandRepository,
            IRepository<CarType> typeRepository,
            IRepository<Car> carRepository,
            IRepository<Customer> customerRepository,
            IRepository<Reservation> reservationRepository,
            IRepository<RentalContract> contractRepository)
        {
            ClassRepository = classRepository;
            BrandRepository = brandRepository;
            TypeRepository = typeRepository;
            CarRepository = carRepository;
            CustomerRepository = customerRepository;
            ReservationRepository = reservationRepository;
            ContractRepository = contractRepository;
        }

        public async Task InitDemoDataAsync()
        {
            var brands = await BrandRepository.GetAllAsync();
            if (brands.Any())
                return; // already initialized

            var brandA = new CarBrand
            {
                Title = "Audi"
            };

            var brandB = new CarBrand
            {
                Title = "BMW"
            };

            var brandC = new CarBrand
            {
                Title = "Fiat"
            };

            brandA = await BrandRepository.AddAsync(brandA);
            brandB = await BrandRepository.AddAsync(brandB);
            brandC = await BrandRepository.AddAsync(brandC);

            var classA = new CarClass
            {
                Title = "Cheap",
                Cost = 102.20M
            };

            var classB = new CarClass
            {
                Title = "Midlevel",
                Cost = 180.50M
            };

            var classC = new CarClass
            {
                Title = "Highend",
                Cost = 250.99M
            };

            classA = await ClassRepository.AddAsync(classA);
            classB = await ClassRepository.AddAsync(classB);
            classC = await ClassRepository.AddAsync(classC);

            var typeA = new CarType
            {
                Title = "PKW"
            };

            var typeB = new CarType
            {
                Title = "Limousine"
            };

            var typeC = new CarType
            {
                Title = "Coupé"
            };

            typeA = await TypeRepository.AddAsync(typeA);
            typeB = await TypeRepository.AddAsync(typeB);
            typeC = await TypeRepository.AddAsync(typeC);

            var customerA = new Customer
            {
                FirstName = "Peter",
                LastName = "Müller",
                Address = "Mordorweg 4, 8355 Gondor",
                EMail = "peter.mueller@gondor.ch",
                PhoneNumber = "79 546 65 65"
            };

            var customerB = new Customer
            {
                FirstName = "Maria",
                LastName = "Meier",
                Address = "Rohangasse 23, 5564 Auenland",
                EMail = "maria.meier@auenland.ch",
                PhoneNumber = "76 215 54 64"
            };

            var customerC = new Customer
            {
                FirstName = "Bruno",
                LastName = "Gander",
                Address = "Isengardweg 3, 5445 Helmsklamm",
                EMail = "bruno.gander@helmsklamm.ch",
                PhoneNumber = "76 651 12 35"
            };

            customerA = await CustomerRepository.AddAsync(customerA);
            customerB = await CustomerRepository.AddAsync(customerB);
            customerC = await CustomerRepository.AddAsync(customerC);

            var carA = new Car
            {
                BrandId = brandA.Id,
                ClassId = classA.Id,
                HorsePower = 112,
                Kilometers = 216535,
                RegistrationYear = 2000,
                TypeId = typeA.Id
            };

            var carB = new Car
            {
                BrandId = brandB.Id,
                ClassId = classB.Id,
                HorsePower = 212,
                Kilometers = 116535,
                RegistrationYear = 2010,
                TypeId = typeB.Id
            };

            var carC = new Car
            {
                BrandId = brandC.Id,
                ClassId = classC.Id,
                HorsePower = 312,
                Kilometers = 16535,
                RegistrationYear = 2018,
                TypeId = typeC.Id
            };

            carA = await CarRepository.AddAsync(carA);
            carB = await CarRepository.AddAsync(carB);
            carC = await CarRepository.AddAsync(carC);

            var reservationA = new Reservation
            {
                CarId = carA.Id,
                CustomerId = customerA.Id,
                Days = 12,
                RentalDate = DateTime.Now.AddDays(12),
                ReservationDate = DateTime.Now,
                State = ReservationState.Pending
            };

            var reservationB = new Reservation
            {
                CustomerId = customerB.Id,
                CarId = carB.Id,
                Days = 2,
                RentalDate = DateTime.Now.AddDays(1),
                ReservationDate = DateTime.Now.AddDays(-3),
                State = ReservationState.Reserved
            };

            var reservationC = new Reservation
            {
                CustomerId = customerC.Id,
                CarId = carC.Id,
                Days = 42,
                RentalDate = DateTime.Now.AddDays(-2),
                ReservationDate = DateTime.Now.AddDays(-8),
                State = ReservationState.Contracted
            };

            await ReservationRepository.AddAsync(reservationA);
            await ReservationRepository.AddAsync(reservationB);
            reservationC = await ReservationRepository.AddAsync(reservationC);

            var contractA = new RentalContract
            {
                CarId = carC.Id,
                CustomerId = customerC.Id,
                Days = 42,
                RentalDate = DateTime.Now.AddDays(-2),
                ReservationId = reservationC.Id,
                TotalCosts = classC.Cost * 42
            };

            await ContractRepository.AddAsync(contractA);

        }
    }
}
