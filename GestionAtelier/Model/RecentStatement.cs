using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class RecentStatement
    {
        public string VehicleId { get; set; }
        public int Hour { get; set; }
        public int Kilometer { get; set; }

        /// <summary>
        /// Constructor RecentStatement
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="hour"></param>
        /// <param name="kilometer"></param>
        public RecentStatement(string vehicleId, int hour, int kilometer)
        {
            VehicleId = vehicleId;
            Hour = hour;
            Kilometer = kilometer;
        }
    }
}
