using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GestionAtelier.Model
{
    class ImageIntervention
    {
        public string Image { get; set; }
        public string Name { get; set; }

        public ImageIntervention(string image, string name)
        {
            Image = image;
            Name = name;
        }
    }
}
