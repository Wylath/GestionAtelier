using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class StatusModel : ObservableObject
    {
        private readonly Status _Status;

        public StatusModel(Status Status)
        {
            _Status = Status;
            RaisePropertyChanged();
        }

        public int StatusId
        {
            get
            {
                return _Status.StatusId;
            }
            set
            {
                if (_Status.StatusId != value)
                    _Status.StatusId = value;
                RaisePropertyChanged(() => StatusId);
            }
        }

        public string Name
        {
            get
            {
                return _Status.Name;
            }
            set
            {
                if (_Status.Name != value)
                    _Status.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
    }
}
