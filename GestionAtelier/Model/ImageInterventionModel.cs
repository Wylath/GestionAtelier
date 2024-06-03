using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GestionAtelier.Model
{
    class ImageInterventionModel : ObservableObject
    {
        private readonly ImageIntervention _ImageIntervention;

        public ImageInterventionModel(ImageIntervention ImageIntervention)
        {
            _ImageIntervention = ImageIntervention;
            RaisePropertyChanged();
        }

        public string Image
        {
            get
            {
                return _ImageIntervention.Image;
            }
            set
            {
                if (_ImageIntervention.Image != value)
                    _ImageIntervention.Image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        public string Name
        {
            get
            {
                return _ImageIntervention.Name;
            }
            set
            {
                if (_ImageIntervention.Name != value)
                    _ImageIntervention.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
    }
}
