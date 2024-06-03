using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.ViewModel
{
    class DetailViewModel<T> : ObservableObject
    {
        private readonly T _Detail;

        public T Detail
        {
            get
            {
                return _Detail;
            }
        }

        public DetailViewModel(T Detail)
        {
            if (Detail == null)
                throw new Exception();

            _Detail = Detail;
        }
    }
}
