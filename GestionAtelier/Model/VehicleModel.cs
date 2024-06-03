using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class VehicleModel : ObservableObject
    {
        private readonly Vehicle _Vehicle;

        public VehicleModel(Vehicle Vehicle)
        {
            _Vehicle = Vehicle;
            RaisePropertyChanged();
        }

        public string VehicleId
        {
            get
            {
                return _Vehicle.VehicleId;
            }
            set
            {
                if (_Vehicle.VehicleId != value)
                    _Vehicle.VehicleId = value;
                RaisePropertyChanged(() => VehicleId);
            }
        }

        public string CompanyId
        {
            get
            {
                return _Vehicle.CompanyId;
            }
            set
            {
                if (_Vehicle.CompanyId != value)
                    _Vehicle.CompanyId = value;
                RaisePropertyChanged(() => CompanyId);
            }
        }

        public string AssetID
        {
            get
            {
                return _Vehicle.AssetID;
            }
            set
            {
                if (_Vehicle.AssetID != value)
                    _Vehicle.AssetID = value;
                RaisePropertyChanged(() => AssetID);
            }
        }

        public string Name
        {
            get
            {
                return _Vehicle.Name;
            }
            set
            {
                if (_Vehicle.Name != value)
                    _Vehicle.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string SerialNum
        {
            get
            {
                return _Vehicle.SerialNum;
            }
            set
            {
                if (_Vehicle.SerialNum != value)
                    _Vehicle.SerialNum = value;
                RaisePropertyChanged(() => SerialNum);
            }
        }

        public string VehicleType
        {
            get
            {
                return _Vehicle.VehicleType;
            }
            set
            {
                if (_Vehicle.VehicleType != value)
                    _Vehicle.VehicleType = value;
                RaisePropertyChanged(() => VehicleType);
            }
        }

        public string Make
        {
            get
            {
                return _Vehicle.Make;
            }
            set
            {
                if (_Vehicle.Make != value)
                    _Vehicle.Make = value;
                RaisePropertyChanged(() => Make);
            }
        }

        public string Model
        {
            get
            {
                return _Vehicle.Model;
            }
            set
            {
                if (_Vehicle.Model != value)
                    _Vehicle.Model = value;
                RaisePropertyChanged(() => Model);
            }
        }

        public string Responsible
        {
            get
            {
                return _Vehicle.Responsible;
            }
            set
            {
                if (_Vehicle.Responsible != value)
                    _Vehicle.Responsible = value;
                RaisePropertyChanged(() => Responsible);
            }
        }

        public string MajorType
        {
            get
            {
                return _Vehicle.MajorType;
            }
            set
            {
                if (_Vehicle.MajorType != value)
                    _Vehicle.MajorType = value;
                RaisePropertyChanged(() => MajorType);
            }
        }

        public string PaintNumber
        {
            get
            {
                return _Vehicle.PaintNumber;
            }
            set
            {
                if (_Vehicle.PaintNumber != value)
                    _Vehicle.PaintNumber = value;
                RaisePropertyChanged(() => PaintNumber);
            }
        }

        public string ModelYear
        {
            get
            {
                return _Vehicle.ModelYear;
            }
            set
            {
                if (_Vehicle.ModelYear != value)
                    _Vehicle.ModelYear = value;
                RaisePropertyChanged(() => ModelYear);
            }
        }

        public string TechInfo1
        {
            get
            {
                return _Vehicle.TechInfo1;
            }
            set
            {
                if (_Vehicle.TechInfo1 != value)
                    _Vehicle.TechInfo1 = value;
                RaisePropertyChanged(() => TechInfo1);
            }
        }

        public string TechInfo2
        {
            get
            {
                return _Vehicle.TechInfo2;
            }
            set
            {
                if (_Vehicle.TechInfo2 != value)
                    _Vehicle.TechInfo2 = value;
                RaisePropertyChanged(() => TechInfo2);
            }
        }

        public string TechInfo3
        {
            get
            {
                return _Vehicle.TechInfo3;
            }
            set
            {
                if (_Vehicle.TechInfo3 != value)
                    _Vehicle.TechInfo3 = value;
                RaisePropertyChanged(() => TechInfo3);
            }
        }

        public int FuelType
        {
            get
            {
                return _Vehicle.FuelType;
            }
            set
            {
                if (_Vehicle.FuelType != value)
                    _Vehicle.FuelType = value;
                RaisePropertyChanged(() => FuelType);
            }
        }

        public int Status
        {
            get
            {
                return _Vehicle.Status;
            }
            set
            {
                if (_Vehicle.Status != value)
                    _Vehicle.Status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public DateTime SalesDate
        {
            get
            {
                return _Vehicle.SalesDate;
            }
            set
            {
                if (_Vehicle.SalesDate != value)
                    _Vehicle.SalesDate = value;
                RaisePropertyChanged(() => SalesDate);
            }
        }

        public DateTime NextControlTechn
        {
            get
            {
                return _Vehicle.NextControlTechn;
            }
            set
            {
                if (_Vehicle.NextControlTechn != value)
                    _Vehicle.NextControlTechn = value;
                RaisePropertyChanged(() => NextControlTechn);
            }
        }

        public string TotalCardNum
        {
            get
            {
                return _Vehicle.TotalCardNum;
            }
            set
            {
                if (_Vehicle.TotalCardNum != value)
                    _Vehicle.TotalCardNum = value;
                RaisePropertyChanged(() => TotalCardNum);
            }
        }

        public string BadgeEpack
        {
            get
            {
                return _Vehicle.BadgeEpack;
            }
            set
            {
                if (_Vehicle.BadgeEpack != value)
                    _Vehicle.BadgeEpack = value;
                RaisePropertyChanged(() => BadgeEpack);
            }
        }

        public string Department
        {
            get
            {
                return _Vehicle.Department;
            }
            set
            {
                if (_Vehicle.Department != value)
                    _Vehicle.Department = value;
                RaisePropertyChanged(() => Department);
            }
        }

        public string CostCenter
        {
            get
            {
                return _Vehicle.CostCenter;
            }
            set
            {
                if (_Vehicle.CostCenter != value)
                    _Vehicle.CostCenter = value;
                RaisePropertyChanged(() => CostCenter);
            }
        }

        public string Purpose
        {
            get
            {
                return _Vehicle.Purpose;
            }
            set
            {
                if (_Vehicle.Purpose != value)
                    _Vehicle.Purpose = value;
                RaisePropertyChanged(() => Purpose);
            }
        }

        public string PlateNumber
        {
            get
            {
                return _Vehicle.PlateNumber;
            }
            set
            {
                if (_Vehicle.PlateNumber != value)
                    _Vehicle.PlateNumber = value;
                RaisePropertyChanged(() => PlateNumber);
            }
        }

        public string PreviousPlateNumber
        {
            get
            {
                return _Vehicle.PreviousPlateNumber;
            }
            set
            {
                if (_Vehicle.PreviousPlateNumber != value)
                    _Vehicle.PreviousPlateNumber = value;
                RaisePropertyChanged(() => PreviousPlateNumber);
            }
        }

        public int IntervalHour
        {
            get
            {
                return _Vehicle.IntervalHour;
            }
            set
            {
                if (_Vehicle.IntervalHour != value)
                    _Vehicle.IntervalHour = value;
                RaisePropertyChanged(() => IntervalHour);
            }
        }

        public int IntervalKilometer
        {
            get
            {
                return _Vehicle.IntervalKilometer;
            }
            set
            {
                if (_Vehicle.IntervalKilometer != value)
                    _Vehicle.IntervalKilometer = value;
                RaisePropertyChanged(() => IntervalKilometer);
            }
        }

        public string Picture
        {
            get
            {
                return _Vehicle.Picture;
            }
            set
            {
                if (_Vehicle.Picture != value)
                    _Vehicle.Picture = value;
                RaisePropertyChanged(() => Picture);
            }
        }

        public string FullName
        {
            get
            {
                return _Vehicle.FullName;
            }
        }
    }
}
