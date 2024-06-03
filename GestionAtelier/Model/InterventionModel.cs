using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class InterventionModel : ObservableObject
    {
        private readonly Intervention _Intervention;

        public InterventionModel(Intervention Intervention)
        {
            _Intervention = Intervention;
            RaisePropertyChanged();
        }

        public int interventionId
        {
            get
            {
                return _Intervention.interventionId;
            }
            set
            {
                if (_Intervention.interventionId != value)
                    _Intervention.interventionId = value;
                RaisePropertyChanged(() => interventionId);
            }
        }

        public Vehicle vehicleId
        {
            get
            {
                return _Intervention.vehicleId;
            }
            set
            {
                if (_Intervention.vehicleId != value)
                    _Intervention.vehicleId = value;
                RaisePropertyChanged(() => vehicleId);
            }
        }

        public InterventionType interventionTypeId
        {
            get
            {
                return _Intervention.interventionTypeId;
            }
            set
            {
                if (_Intervention.interventionTypeId != value)
                    _Intervention.interventionTypeId = value;
                RaisePropertyChanged(() => interventionTypeId);
            }
        }

        public Employee Submitter
        {
            get
            {
                return _Intervention.Submitter;
            }
            set
            {
                if (_Intervention.Submitter != value)
                    _Intervention.Submitter = value;
                RaisePropertyChanged(() => Submitter);
            }
        }

        public Employee Applicant
        {
            get
            {
                return _Intervention.Applicant;
            }
            set
            {
                if (_Intervention.Applicant != value)
                    _Intervention.Applicant = value;
                RaisePropertyChanged(() => Applicant);
            }
        }

        public Status StatusId
        {
            get
            {
                return _Intervention.StatusId;
            }
            set
            {
                if (_Intervention.StatusId != value)
                    _Intervention.StatusId = value;
                RaisePropertyChanged(() => StatusId);
            }
        }

        public DateTime StatusDate
        {
            get
            {
                return _Intervention.StatusDate;
            }
            set
            {
                if (_Intervention.StatusDate != value)
                    _Intervention.StatusDate = value;
                RaisePropertyChanged(() => StatusDate);
            }
        }

        public DateTime DateIn
        {
            get
            {
                return _Intervention.DateIn;
            }
            set
            {
                if (_Intervention.DateIn != value)
                    _Intervention.DateIn = value;
                RaisePropertyChanged(() => DateIn);
            }
        }

        public DateTime DateOut
        {
            get
            {
                return _Intervention.DateOut;
            }
            set
            {
                if (_Intervention.DateOut != value)
                    _Intervention.DateOut = value;
                RaisePropertyChanged(() => DateOut);
            }
        }

        public DateTime DateEstimate
        {
            get
            {
                return _Intervention.DateEstimate;
            }
            set
            {
                if (_Intervention.DateEstimate != value)
                    _Intervention.DateEstimate = value;
                RaisePropertyChanged(() => DateEstimate);
            }
        }

        public bool PieceOrder
        {
            get
            {
                return _Intervention.PieceOrder;
            }
            set
            {
                if (_Intervention.PieceOrder != value)
                    _Intervention.PieceOrder = value;
                RaisePropertyChanged(() => PieceOrder);
            }
        }

        public string PieceCom
        {
            get
            {
                return _Intervention.PieceCom;
            }
            set
            {
                if (_Intervention.PieceCom != value)
                    _Intervention.PieceCom = value;
                RaisePropertyChanged(() => PieceCom);
            }
        }

        public float TimeEstimate
        {
            get
            {
                return _Intervention.TimeEstimate;
            }
            set
            {
                if (_Intervention.TimeEstimate != value)
                    _Intervention.TimeEstimate = value;
                RaisePropertyChanged(() => TimeEstimate);
            }
        }

        public Priority Priority
        {
            get
            {
                return _Intervention.Priority;
            }
            set
            {
                if (_Intervention.Priority != value)
                    _Intervention.Priority = value;
                RaisePropertyChanged(() => Priority);
            }
        }

        public ParmSite SiteId
        {
            get
            {
                return _Intervention.SiteId;
            }
            set
            {
                if (_Intervention.SiteId != value)
                    _Intervention.SiteId = value;
                RaisePropertyChanged(() => SiteId);
            }
        }

        public List<Prestation> Prestations
        {
            get
            {
                return _Intervention.Prestations;
            }
            set
            {
                if (_Intervention.Prestations != value)
                    _Intervention.Prestations = value;
                RaisePropertyChanged(() => Prestations);
            }
        }

        public int Barcode
        {
            get
            {
                return _Intervention.Barcode;
            }
            set
            {
                if (_Intervention.Barcode != value)
                    _Intervention.Barcode = value;
                RaisePropertyChanged(() => Barcode);
            }
        }

        public string barcodePicture
        {
            get
            {
                return _Intervention.barcodePicture;
            }
            set
            {
                if (_Intervention.barcodePicture != value)
                    _Intervention.barcodePicture = value;
                RaisePropertyChanged(() => barcodePicture);
            }
        }

        public bool IsCheckedForGenerateSheet
        {
            get
            {
                return _Intervention.IsCheckedForGenerateSheet;
            }
            set
            {
                if (_Intervention.IsCheckedForGenerateSheet != value)
                    _Intervention.IsCheckedForGenerateSheet = value;
                RaisePropertyChanged(() => IsCheckedForGenerateSheet);
            }
        }
    }
}
