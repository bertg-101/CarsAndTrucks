using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLibrary
{
    public class VehicleManager
    {
        /// <summary>
        /// Vehicles
        /// </summary>
        public List<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Vehicle Manager
        /// </summary>
        public VehicleManager() {}

        /// <summary>
        /// Fastest vehicles, but skip the first vehicle, use kmh
        /// </summary>
        /// <param name="vehicles"></param>
        /// <returns></returns>
        public List<Vehicle> FastVehicles(List<Vehicle> vehicles)
        {
            // var v = vehicles.Where(m=>m.VehicleType.ToLower()=="car").Take(4).Skip(1)).ToList();
            var carList = vehicles.Where(m => m.VehicleType.ToLower() == "car");
            foreach (var v in carList)
            {
                if (v.CountryOfOrigin == "USA")
                {
                    v.TopSpeed = MPHtoKmPH(v.TopSpeed);
                }
            }

            return (carList.OrderBy(m=>m.TopSpeed).Take(4).Skip(1)).ToList();
        }

        /// <summary>
        /// Weight capacity is dependent on car or truck type AND trunk/cargo sq ft
        ///   ALTERNATIVE: Subclass the car and truck to a vehicle, and override the calculation
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Int64 WeightCapacity(Vehicle v)
        {
            // car: weight capacity = (no passsengers * 275 lbs) + (trunk sq ft * 40 lbs)
            // truck: weight capacity = (no passengers * 300 lbs) + (cargo sq ft * 50 lbs)
            Int64 result = 0;
            switch (v.VehicleType.ToLower())
            {
                case "car":
                    result = (v.PassengerCapacity * 275) + ((v.TrunkSqFt ?? 0) * 40);
                    break;
                case "truck":
                    result = (v.PassengerCapacity * 300) + ((v.CargoSqFt ?? 0) * 50);
                    break;
                default:
                    result = 0;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Calculate average top speed for European cars based on a filter
        /// </summary>
        /// <param name="vehicleList"></param>
        /// <param name="KmPH"></param>
        /// <returns></returns>
        public double TopSpeedForEuropeanCars(List<Vehicle> vehicleList, bool KmPH)
        {
            // ALTERNATIVE: USE A LOOKUP TO FIND IMPORTS
            //   THEN CALC AVG SPEED FOR THOSE
            //var importLookup = ImportStyle();
            //foreach (var c in vehicleList)
            //{
            //    if (importLookup.ContainsKey(c.CountryOfOrigin.ToLower()))
            //    {
            //        // find all types that match lookup
            //    }
            //}

            // USE A LINQ - HARD CODE Models
            var averageTopSpeed = (from c in Vehicles
                                   where ((c.Make.ToLower()=="bmw")||(c.Make.ToLower()=="audi")||(c.Make.ToLower()=="volvo"))
                                   select c.TopSpeed).Average();
            if (KmPH)
                { return KmPHtoMPH(averageTopSpeed);  }

            return averageTopSpeed;
        }

        /// <summary>
        /// Convert kilometers-per-hour to miles-per-hour
        /// </summary>
        /// <param name="kmPerHour"></param>
        /// <returns></returns>
        public double KmPHtoMPH(double kmPerHour)
        {
            return (kmPerHour * .621371);
        }

        /// <summary>
        /// Convert miles-per-hour to km-per-hour
        /// </summary>
        /// <param name="MPH"></param>
        /// <returns></returns>
        public double MPHtoKmPH(double MPH)
        {
            return (MPH * 1.06934);
        }

        /// <summary>
        /// Dictionary if using a key/value pair lookup
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ImportStyle ()
        {
            Dictionary<string, string> importLookup = new Dictionary<string, string>();
            importLookup.Add("germany", "european");
            importLookup.Add("sweden", "european");
            importLookup.Add("usa", "domestic");
            importLookup.Add("japan", "asian");

            return importLookup;
        }
    }
}
