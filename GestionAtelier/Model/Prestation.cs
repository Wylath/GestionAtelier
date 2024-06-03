using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class Prestation
    {
        public int PrestationId { get; set; }
        public Intervention InterventionId { get; set; }
        public float HoursCount { get; set; }
        public Employee EmployeeId { get; set; }
        public PrestationType PrestationTypeId { get; set; }
        public DateTime Date { get; set; }
        public int HourVehicle { get; set; }
        public int KilometerVehicle { get; set; }
        public int TruckCrane { get; set; }
        public bool Realized { get; set; }
        public string Comment { get; set; }
        public bool Truck { get; set; }
        public bool Crane { get; set; }
        public bool IsCheckedForGenerateSheet { get; set; }
        public int HourFuel { get; set; }
        public int KilometerFuel { get; set; }
        public DateTime DateFuel { get; set; }

        /// <summary>
        /// Constructor prestation
        /// </summary>
        /// <param name="prestationId"></param>
        /// <param name="interventionId"></param>
        /// <param name="badgeEmployee"></param>
        /// <param name="hoursCount"></param>
        /// <param name="employeeId"></param>
        /// <param name="prestationTypeId"></param>
        /// <param name="date"></param>
        public Prestation(PrestationType prestationTypeId, DateTime Date)
        {
            PrestationTypeId = prestationTypeId;
            this.Date = Date;
        }

        /// <summary>
        /// Constructor prestation
        /// </summary>
        /// <param name="prestationId"></param>
        /// <param name="interventionId"></param>
        /// <param name="badgeEmployee"></param>
        /// <param name="hoursCount"></param>
        /// <param name="employeeId"></param>
        /// <param name="prestationTypeId"></param>
        /// <param name="date"></param>
        public Prestation(PrestationType prestationTypeId, DateTime Date, DateTime DateFuel, int hourFuel, int kilometerFuel)
        {
            PrestationTypeId = prestationTypeId;
            this.Date = Date;
            this.DateFuel = DateFuel;
            HourFuel = hourFuel;
            KilometerFuel = kilometerFuel;
        }

        /// <summary>
        /// Constructor prestation
        /// </summary>
        /// <param name="prestationId"></param>
        /// <param name="interventionId"></param>
        /// <param name="badgeEmployee"></param>
        /// <param name="hoursCount"></param>
        /// <param name="employeeId"></param>
        /// <param name="prestationTypeId"></param>
        /// <param name="date"></param>
        public Prestation(int prestationId, Intervention interventionId, float hoursCount, Employee employeeId, PrestationType prestationTypeId, DateTime date)
        {
            PrestationId = prestationId;
            InterventionId = interventionId;
            HoursCount = hoursCount;
            EmployeeId = employeeId;
            PrestationTypeId = prestationTypeId;
            Date = date;
        }

        public Prestation(int prestationId, Intervention interventionId, float hoursCount, Employee employeeId, PrestationType prestationTypeId, DateTime date, int truckCrane, int HourVehicle, int KilometerVehicle) : this(prestationId, interventionId, hoursCount, employeeId, prestationTypeId, date)
        {
            TruckCrane = truckCrane;
            this.HourVehicle = HourVehicle;
            this.KilometerVehicle = KilometerVehicle;
        }

        public Prestation(int prestationId, Intervention interventionId, float hoursCount, Employee employeeId, PrestationType prestationTypeId, DateTime date, int truckCrane, int hourVehicle, int kilometerVehicle, int hourFuel, int kilometerFuel, DateTime dateFuel) : this(prestationId, interventionId, hoursCount, employeeId, prestationTypeId, date, hourVehicle, kilometerVehicle, truckCrane)
        {
            HourFuel = hourFuel;
            KilometerFuel = kilometerFuel;
            DateFuel = dateFuel;
        }
    }
}
