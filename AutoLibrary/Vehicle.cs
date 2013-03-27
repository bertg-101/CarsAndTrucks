using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLibrary
{
    /// <summary>
    /// Vehicle
    /// </summary>
    public class Vehicle
    {
        public string Make { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleType { get; set; }
        public string CountryOfOrigin { get; set; }
        public double TopSpeed { get; set; }
        public Int64 PassengerCapacity { get; set; }
        public Int64? CargoSqFt { get; set; }
        public Int64? TrunkSqFt { get; set; }
    }
}
