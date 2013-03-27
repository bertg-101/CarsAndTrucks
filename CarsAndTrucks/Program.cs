using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLibrary;

namespace CarsAndTrucks
{
    /// <summary>
    /// Cars and Trucks
    /// -- Load a list of cars and trucks
    /// -- Perform calculations
    /// -- Display the results
    /// -- Possible enhancements:
    ///     1) add a unit test project to test Vehicle Manager methods
    ///     2) add a bootstrapper that loads the vehicles from a JSON file
    ///     3) subclass the car and truck classes, overriding methods and MPH vs KmH
    /// </summary>
    class Program
    {
        // Vehicle manager
        static VehicleManager vehicleManager;

        /// <summary>
        /// Main 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            BootstrapCatalog();
            // display the top speed
            DisplayTopSpeed(vehicleManager);
            Console.ReadLine();

            DisplayWeightCapacity(vehicleManager);
            Console.ReadLine();

            DisplayFastestCars(vehicleManager);
            Console.WriteLine("Press any key to exit the app...");
            Console.ReadLine();
        }

        /// <summary>
        /// Display the Average Top Speed for Euro Cars in MPH
        /// </summary>
        /// <param name="vm"></param>
        public static void DisplayTopSpeed(VehicleManager vm)
        {
            Console.WriteLine(
                String.Format("Top Average Speed for European cars is {0} MPH",
                vm.TopSpeedForEuropeanCars(vm.Vehicles, true).ToString("F2")));
            Console.WriteLine("====");
        }

        /// <summary>
        /// Display the Weight Capacity for cars and trucks
        /// </summary>
        /// <param name="vm"></param>
        public static void DisplayWeightCapacity(VehicleManager vm)
        {
            Console.WriteLine("Weight Capacities");
            foreach (var v in vm.Vehicles)
            {
                Console.WriteLine(
                    String.Format("Weight capacity for {0} {1} is {2}.",
                    v.Make, v.VehicleModel,
                    vm.WeightCapacity(v)));
                Console.WriteLine("---");
            }
            Console.WriteLine("====");
        }

        /// <summary>
        /// Display the Fastest Cars, except the fastest
        /// </summary>
        /// <param name="vm"></param>
        public static void DisplayFastestCars(VehicleManager vm)
        {
            List<Vehicle> vehicles = vm.FastVehicles(vm.Vehicles);
            Console.WriteLine("Fastest vehicles (except fastest)");
            foreach (var v in vehicles)
            {
                Console.WriteLine(
                    String.Format("{0} {1} top speed at {2} kmh",
                    v.Make, v.VehicleModel, v.TopSpeed));
            }
            Console.WriteLine("====");

        }   

        /// <summary>
        /// Fill the Catalog
        ///   ALTERNATIVE: Serialize/Deserialize object to JSON, to load from a data source
        /// </summary>
        static void BootstrapCatalog()
        {
            VehicleManager vm = new VehicleManager();

            vm.Vehicles = new List<Vehicle> {
                new Vehicle { Make="BMW", VehicleModel="135i", VehicleType="Car", CountryOfOrigin="Germany", TopSpeed=193, PassengerCapacity=4, TrunkSqFt= 13 },
                new Vehicle { Make="Audi", VehicleModel="A5", VehicleType="Car", CountryOfOrigin="Germany", TopSpeed=209, PassengerCapacity=6, TrunkSqFt= 18 },
                new Vehicle { Make="Volvo", VehicleModel="S80", VehicleType="Car", CountryOfOrigin="Sweden", TopSpeed=145, PassengerCapacity=6, TrunkSqFt= 17 },
                new Vehicle { Make="Honda", VehicleModel="RidgeLine", VehicleType="Truck", CountryOfOrigin="Japan", TopSpeed=120, PassengerCapacity=3, CargoSqFt= 30 },
                new Vehicle { Make="Honda", VehicleModel="Accord", VehicleType="Car", CountryOfOrigin="Japan", TopSpeed=135, PassengerCapacity=6, TrunkSqFt= 16 },
                new Vehicle { Make="Chevrolet", VehicleModel="Corvette", VehicleType="Car", CountryOfOrigin="USA", TopSpeed=225, PassengerCapacity=2, TrunkSqFt= 4 },
                new Vehicle { Make="Chevrolet", VehicleModel="Silverado", VehicleType="Truck", CountryOfOrigin="USA", TopSpeed=80, PassengerCapacity=3, CargoSqFt= 35 },
                new Vehicle { Make="Ford", VehicleModel="F-350", VehicleType="Truck", CountryOfOrigin="USA", TopSpeed=95, PassengerCapacity=5, CargoSqFt= 36 }
            };

            vehicleManager = vm;
        }
    }
}
