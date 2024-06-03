using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class Fuel
    {
        public int RECID { get; set; }
        public string VehicleId { get; set; }
        public int Kilometer { get; set; }
        public int Hour { get; set; }
        public DateTime DatePrise { get; set; }

        /// <summary>
        /// Construtor for the classe fuel
        /// </summary>
        /// <param name="rECID"></param>
        /// <param name="vehicleId"></param>
        /// <param name="kilometer"></param>
        /// <param name="hour"></param>
        /// <param name="datePrise"></param>
        public Fuel(int rECID, string vehicleId, int kilometer, int hour, DateTime datePrise)
        {
            RECID = rECID;
            VehicleId = vehicleId;
            Kilometer = kilometer;
            Hour = hour;
            DatePrise = datePrise;
        }
    }
}
