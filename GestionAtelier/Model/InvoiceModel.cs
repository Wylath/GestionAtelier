using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class InvoiceModel : ObservableObject
    {
        private readonly Invoice _Invoice;

        public InvoiceModel(Invoice Invoice)
        {
            _Invoice = Invoice;
            RaisePropertyChanged();
        }

        public int InvoiceId
        {
            get
            {
                return _Invoice.InvoiceId;
            }
            set
            {
                if (_Invoice.InvoiceId != value)
                    _Invoice.InvoiceId = value;
                RaisePropertyChanged(() => InvoiceId);
            }
        }

        public float Ponderation
        {
            get
            {
                return _Invoice.Ponderation;
            }
            set
            {
                if (_Invoice.Ponderation != value)
                    _Invoice.Ponderation = value;
                RaisePropertyChanged(() => Ponderation);
            }
        }

        public int TVA
        {
            get
            {
                return _Invoice.TVA;
            }
            set
            {
                if (_Invoice.TVA != value)
                    _Invoice.TVA = value;
                RaisePropertyChanged(() => TVA);
            }
        }

        public int Price
        {
            get
            {
                return _Invoice.Price;
            }
            set
            {
                if (_Invoice.Price != value)
                    _Invoice.Price = value;
                RaisePropertyChanged(() => Price);
            }
        }

        public int NumberInvoice
        {
            get
            {
                return _Invoice.NumberInvoice;
            }
            set
            {
                if (_Invoice.NumberInvoice != value)
                    _Invoice.NumberInvoice = value;
                RaisePropertyChanged(() => NumberInvoice);
            }
        }

        public DateTime DateInvoice
        {
            get
            {
                return _Invoice.DateInvoice;
            }
            set
            {
                if (_Invoice.DateInvoice != value)
                    _Invoice.DateInvoice = value;
                RaisePropertyChanged(() => DateInvoice);
            }
        }

        public List<Prestation> prestation
        {
            get
            {
                return _Invoice.prestation;
            }
            set
            {
                if (_Invoice.prestation != value)
                    _Invoice.prestation = value;
                RaisePropertyChanged(() => prestation);
            }
        }

        public float TotalPrice
        {
            get
            {
                return _Invoice.TotalPrice;
            }
            set
            {
                if (_Invoice.TotalPrice != value)
                    _Invoice.TotalPrice = value;
                RaisePropertyChanged(() => TotalPrice);
            }
        }

        public float ValuePriceTva
        {
            get
            {
                return _Invoice.ValuePriceTva;
            }
            set
            {
                if (_Invoice.ValuePriceTva != value)
                    _Invoice.ValuePriceTva = value;
                RaisePropertyChanged(() => ValuePriceTva);
            }
        }

        public float TotalPriceWithTva
        {
            get
            {
                return _Invoice.TotalPriceWithTva;
            }
            set
            {
                if (_Invoice.TotalPriceWithTva != value)
                    _Invoice.TotalPriceWithTva = value;
                RaisePropertyChanged(() => TotalPriceWithTva);
            }
        }
    }
}
