using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class PrestationTypeModel : ObservableObject
    {
        private readonly PrestationType _PrestationType;

        public PrestationTypeModel(PrestationType PrestationType)
        {
            _PrestationType = PrestationType;
            RaisePropertyChanged();
        }

        public int PrestationTypeId
        {
            get
            {
                return _PrestationType.PrestationTypeId;
            }
            set
            {
                if (_PrestationType.PrestationTypeId != value)
                    _PrestationType.PrestationTypeId = value;
                RaisePropertyChanged(() => PrestationTypeId);
            }
        }

        public string Name
        {
            get
            {
                return _PrestationType.Name;
            }
            set
            {
                if (_PrestationType.Name != value)
                    _PrestationType.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int Status
        {
            get
            {
                return _PrestationType.Status;
            }
            set
            {
                if (_PrestationType.Status != value)
                    _PrestationType.Status = value;
                RaisePropertyChanged(() => Status);
            }
        }
    }
}
