using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class PriorityModel : ObservableObject
    {
        private readonly Priority _Priority;

        public PriorityModel(Priority Priority)
        {
            _Priority = Priority;
            RaisePropertyChanged();
        }

        public string Name
        {
            get
            {
                return _Priority.Name;
            }
            set
            {
                if (_Priority.Name != value)
                    _Priority.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
    }
}
