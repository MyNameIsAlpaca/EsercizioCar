using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsercizioCar.StoreData;

namespace EsercizioCar.StoreData
{
    
        internal class Car
        {
            public string Id { get; set; }
            public string Company { get; set; }
            public string Model { get; set; }
            public int ProductYear { get; set; }
            public List<CarCharateristic> carCharateristics { get; set; } = new List<CarCharateristic>();

        public Car(string IdCar, string CompanyCar, string ModelCar, int ProductYearCar, List<Car> carList, CarCharateristic? carChar = null)
            {
                Id = IdCar;
                Company = CompanyCar;
                Model = ModelCar;
                ProductYear = ProductYearCar;
                carCharateristics.Add(carChar);
                carList.Add(this);
            }
        }

        internal class CarCharateristic
        {
            public int Id { get; set; }
            public int CarEngine { get; set; }
            public string CarId { get; set; }
            public string CarFuel { get; set; }

            public CarCharateristic(string Fuel, int Engine, string originalCarId, int iD)
            {
                Id = iD;
                CarEngine = Engine;
                CarFuel = Fuel;
                CarId = originalCarId;
            }
        }
    
}
