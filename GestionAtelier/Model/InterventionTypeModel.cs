using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class InterventionTypeModel : ObservableObject
    {
        private readonly InterventionType _InterventionType;

        public InterventionTypeModel(InterventionType InterventionType)
        {
            _InterventionType = InterventionType;
            RaisePropertyChanged();
        }

        public int InterventionTypeId
        {
            get
            {
                return _InterventionType.InterventionTypeId;
            }
            set
            {
                if (_InterventionType.InterventionTypeId != value)
                    _InterventionType.InterventionTypeId = value;
                RaisePropertyChanged(() => InterventionTypeId);
            }
        }

        public string Name
        {
            get
            {
                return _InterventionType.Name;
            }
            set
            {
                if (_InterventionType.Name != value)
                    _InterventionType.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int Status
        {
            get
            {
                return _InterventionType.Status;
            }
            set
            {
                if (_InterventionType.Status != value)
                    _InterventionType.Status = value;
                RaisePropertyChanged(() => Status);
            }
        }
    }
}
