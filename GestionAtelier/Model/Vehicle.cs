using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class Vehicle
    {
        public string VehicleId { get; set; }
        public string CompanyId { get; set; }
        public string AssetID { get; set; }
        public string Name { get; set; }
        public string SerialNum { get; set; }
        public string VehicleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Responsible { get; set; }
        public string MajorType { get; set; }
        public string PaintNumber { get; set; }
        public string ModelYear { get; set; }
        public string TechInfo1 { get; set; }
        public string TechInfo2 { get; set; }
        public string TechInfo3 { get; set; }
        public int FuelType { get; set; }
        public int Status { get; set; }
        public DateTime SalesDate { get; set; }
        public DateTime NextControlTechn { get; set; }
        public string TotalCardNum { get; set; }
        public string BadgeEpack { get; set; }
        public string Department { get; set; }
        public string CostCenter { get; set; }
        public string Purpose { get; set; }
        public string PlateNumber { get; set; }
        public string PreviousPlateNumber { get; set; }
        public int IntervalHour { get; set; }
        public int IntervalKilometer { get; set; }
        public string Picture { get; set; }
        public string FullName { get { return String.Format("{0}: {1}", VehicleId, Name); } }

        /// <summary>
        /// Constructor base vehicle
        /// </summary>
        /// <param name="vehicleId"></param>
        public Vehicle(string vehicleId)
        {
            VehicleId = vehicleId;
        }

        /// <summary>
        /// Constructor Vehicle
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="companyId"></param>
        /// <param name="assetID"></param>
        /// <param name="name"></param>
        /// <param name="serialNum"></param>
        /// <param name="vehicleType"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="responsible"></param>
        /// <param name="majorType"></param>
        /// <param name="paintNumber"></param>
        /// <param name="modelYear"></param>
        /// <param name="techInfo1"></param>
        /// <param name="techInfo2"></param>
        /// <param name="techInfo3"></param>
        /// <param name="fuelType"></param>
        /// <param name="status"></param>
        /// <param name="salesDate"></param>
        /// <param name="nextControlTechn"></param>
        /// <param name="totalCardNum"></param>
        /// <param name="badgeEpack"></param>
        /// <param name="department"></param>
        /// <param name="costCenter"></param>
        /// <param name="purpose"></param>
        /// <param name="plateNumber"></param>
        /// <param name="previousPlateNumber"></param>
        /// <param name="intervalHour"></param>
        /// <param name="intervalKilometer"></param>
        /// <param name="dateHour"></param>
        /// <param name="dateKilometer"></param>
        public Vehicle(string vehicleId, string companyId, string assetID, string name, string serialNum, string vehicleType, string make, string model, string responsible, string majorType, string paintNumber, string modelYear, string techInfo1, string techInfo2, string techInfo3, int fuelType, int status, DateTime salesDate, DateTime nextControlTechn, string totalCardNum, string badgeEpack, string department, string costCenter, string purpose, string plateNumber, string previousPlateNumber, int intervalHour, int intervalKilometer)
        {
            VehicleId = vehicleId;
            CompanyId = companyId;
            AssetID = assetID;
            Name = name;
            SerialNum = serialNum;
            VehicleType = vehicleType;
            Make = make;
            Model = model;
            Responsible = responsible;
            MajorType = majorType;
            PaintNumber = paintNumber;
            ModelYear = modelYear;
            TechInfo1 = techInfo1;
            TechInfo2 = techInfo2;
            TechInfo3 = techInfo3;
            FuelType = fuelType;
            Status = status;
            SalesDate = salesDate;
            NextControlTechn = nextControlTechn;
            TotalCardNum = totalCardNum;
            BadgeEpack = badgeEpack;
            Department = department;
            CostCenter = costCenter;
            Purpose = purpose;
            PlateNumber = plateNumber;
            PreviousPlateNumber = previousPlateNumber;
            IntervalHour = intervalHour;
            IntervalKilometer = intervalKilometer;
        }
    }
}
