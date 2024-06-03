using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class PrestationModel : ObservableObject
    {
        private readonly Prestation _Prestation;

        public PrestationModel(Prestation Prestation)
        {
            _Prestation = Prestation;
            RaisePropertyChanged();
        }

        public int PrestationId
        {
            get
            {
                return _Prestation.PrestationId;
            }
            set
            {
                if (_Prestation.PrestationId != value)
                    _Prestation.PrestationId = value;
                RaisePropertyChanged(() => PrestationId);
            }
        }

        public Intervention InterventionId
        {
            get
            {
                return _Prestation.InterventionId;
            }
            set
            {
                if (_Prestation.InterventionId != value)
                    _Prestation.InterventionId = value;
                RaisePropertyChanged(() => InterventionId);
            }
        }

        public float HoursCount
        {
            get
            {
                return _Prestation.HoursCount;
            }
            set
            {
                if (_Prestation.HoursCount != value)
                    _Prestation.HoursCount = value;
                if (value > 0.00f)
                    Realized = true;
                RaisePropertyChanged(() => HoursCount);
            }
        }

        public Employee EmployeeId
        {
            get
            {
                return _Prestation.EmployeeId;
            }
            set
            {
                if (_Prestation.EmployeeId != value)
                    _Prestation.EmployeeId = value;
                RaisePropertyChanged(() => EmployeeId);
            }
        }

        public PrestationType PrestationTypeId
        {
            get
            {
                return _Prestation.PrestationTypeId;
            }
            set
            {
                if (_Prestation.PrestationTypeId != value)
                    _Prestation.PrestationTypeId = value;
                RaisePropertyChanged(() => PrestationTypeId);
            }
        }

        public DateTime Date
        {
            get
            {
                return _Prestation.Date;
            }
            set
            {
                if (_Prestation.Date != value)
                    _Prestation.Date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        public int TruckCrane
        {
            get
            {
                return _Prestation.TruckCrane;
            }
            set
            {
                if (_Prestation.TruckCrane != value)
                    _Prestation.TruckCrane = value;
                RaisePropertyChanged(() => TruckCrane);
            }
        }

        public int HourVehicle
        {
            get
            {
                return _Prestation.HourVehicle;
            }
            set
            {
                if (_Prestation.HourVehicle != value)
                    _Prestation.HourVehicle = value;
                RaisePropertyChanged(() => HourVehicle);
            }
        }

        public int KilometerVehicle
        {
            get
            {
                return _Prestation.KilometerVehicle;
            }
            set
            {
                if (_Prestation.KilometerVehicle != value)
                    _Prestation.KilometerVehicle = value;
                RaisePropertyChanged(() => KilometerVehicle);
            }
        }

        public bool Realized
        {
            get
            {
                return _Prestation.Realized;
            }
            set
            {
                if (_Prestation.Realized != value)
                    _Prestation.Realized = value;
                RaisePropertyChanged(() => Realized);
            }
        }

        public string Comment
        {
            get
            {
                return _Prestation.Comment;
            }
            set
            {
                if (_Prestation.Comment != value)
                    _Prestation.Comment = value;
                RaisePropertyChanged(() => Comment);
            }
        }

        public bool Truck
        {
            get
            {
                return _Prestation.Truck;
            }
            set
            {
                if (_Prestation.Truck != value)
                    _Prestation.Truck = value;
                RaisePropertyChanged(() => Truck);
            }
        }

        public bool Crane
        {
            get
            {
                return _Prestation.Crane;
            }
            set
            {
                if (_Prestation.Crane != value)
                    _Prestation.Crane = value;
                RaisePropertyChanged(() => Crane);
            }
        }

        public bool IsCheckedForGenerateSheet
        {
            get
            {
                return _Prestation.IsCheckedForGenerateSheet;
            }
            set
            {
                if (_Prestation.IsCheckedForGenerateSheet != value)
                    _Prestation.IsCheckedForGenerateSheet = value;
                RaisePropertyChanged(() => IsCheckedForGenerateSheet);
            }
        }

        public int HourFuel
        {
            get
            {
                return _Prestation.HourFuel;
            }
            set
            {
                if (_Prestation.HourFuel != value)
                    _Prestation.HourFuel = value;
                RaisePropertyChanged(() => HourFuel);
            }
        }

        public int KilometerFuel
        {
            get
            {
                return _Prestation.KilometerFuel;
            }
            set
            {
                if (_Prestation.KilometerFuel != value)
                    _Prestation.KilometerFuel = value;
                RaisePropertyChanged(() => KilometerFuel);
            }
        }

        public DateTime DateFuel
        {
            get
            {
                return _Prestation.DateFuel;
            }
            set
            {
                if (_Prestation.DateFuel != value)
                    _Prestation.DateFuel = value;
                RaisePropertyChanged(() => DateFuel);
            }
        }
    }
}
