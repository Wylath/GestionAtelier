using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class TiresModel : ObservableObject
    {
        private readonly Tires _Tires;

        public TiresModel(Tires Tires)
        {
            _Tires = Tires;
            RaisePropertyChanged();
        }

        public int RECID
        {
            get
            {
                return _Tires.RECID;
            }
            set
            {
                if (_Tires.RECID != value)
                    _Tires.RECID = value;
                RaisePropertyChanged(() => RECID);
            }
        }

        public string CompanyId
        {
            get
            {
                return _Tires.CompanyId;
            }
            set
            {
                if (_Tires.CompanyId != value)
                    _Tires.CompanyId = value;
                RaisePropertyChanged(() => CompanyId);
            }
        }

        public string ItemId
        {
            get
            {
                return _Tires.ItemId;
            }
            set
            {
                if (_Tires.ItemId != value)
                    _Tires.ItemId = value;
                RaisePropertyChanged(() => ItemId);
            }
        }

        public string Dim1
        {
            get
            {
                return _Tires.Dim1;
            }
            set
            {
                if (_Tires.Dim1 != value)
                    _Tires.Dim1 = value;
                RaisePropertyChanged(() => Dim1);
            }
        }

        public string Dim2
        {
            get
            {
                return _Tires.Dim2;
            }
            set
            {
                if (_Tires.Dim2 != value)
                    _Tires.Dim2 = value;
                RaisePropertyChanged(() => Dim2);
            }
        }

        public string Dim3
        {
            get
            {
                return _Tires.Dim3;
            }
            set
            {
                if (_Tires.Dim3 != value)
                    _Tires.Dim3 = value;
                RaisePropertyChanged(() => Dim3);
            }
        }

        public string BarCode
        {
            get
            {
                return _Tires.BarCode;
            }
            set
            {
                if (_Tires.BarCode != value)
                    _Tires.BarCode = value;
                RaisePropertyChanged(() => BarCode);
            }
        }

        public string Comment
        {
            get
            {
                return _Tires.Comment;
            }
            set
            {
                if (_Tires.Comment != value)
                    _Tires.Comment = value;
                RaisePropertyChanged(() => Comment);
            }
        }

        public string ItemType
        {
            get
            {
                return _Tires.ItemType;
            }
            set
            {
                if (_Tires.ItemType != value)
                    _Tires.ItemType = value;
                RaisePropertyChanged(() => ItemType);
            }
        }
    }
}
