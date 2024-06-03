using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class InterventionTireModel : ObservableObject
    {
        private readonly InterventionTire _InterventionTire;

        public InterventionTireModel(InterventionTire InterventionTire)
        {
            _InterventionTire = InterventionTire;
            RaisePropertyChanged();
        }

        public int InterventionTireId
        {
            get
            {
                return _InterventionTire.InterventionTireId;
            }
            set
            {
                if (_InterventionTire.InterventionTireId != value)
                    _InterventionTire.InterventionTireId = value;
                RaisePropertyChanged(() => InterventionTireId);
            }
        }

        public int TireNumber
        {
            get
            {
                return _InterventionTire.TireNumber;
            }
            set
            {
                if (_InterventionTire.TireNumber != value)
                    _InterventionTire.TireNumber = value;
                RaisePropertyChanged(() => TireNumber);
            }
        }

        public int ChangeCause
        {
            get
            {
                return _InterventionTire.ChangeCause;
            }
            set
            {
                if (_InterventionTire.ChangeCause != value)
                    _InterventionTire.ChangeCause = value;
                RaisePropertyChanged(() => ChangeCause);
            }
        }

        public int InterventionId
        {
            get
            {
                return _InterventionTire.InterventionId;
            }
            set
            {
                if (_InterventionTire.InterventionId != value)
                    _InterventionTire.InterventionId = value;
                RaisePropertyChanged(() => InterventionId);
            }
        }

        public Tires NewTireBarcodeId
        {
            get
            {
                return _InterventionTire.NewTireBarcodeId;
            }
            set
            {
                if (_InterventionTire.NewTireBarcodeId != value)
                    _InterventionTire.NewTireBarcodeId = value;
                RaisePropertyChanged(() => NewTireBarcodeId);
            }
        }
    }
}
