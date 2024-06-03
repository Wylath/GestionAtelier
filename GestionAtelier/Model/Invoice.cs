using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class Invoice
    {
        public int InvoiceId { get; set; }
        public float Ponderation { get; set; }
        public int TVA { get; set; }
        public int Price { get; set; }
        public int NumberInvoice { get; set; }
        public DateTime DateInvoice { get; set; }
        public List<Prestation> prestation { get; set; }
        public float TotalPrice { get; set; }
        public float ValuePriceTva { get; set; }
        public float TotalPriceWithTva { get; set; }

        /// <summary>
        /// Constructor Invoice (facture)
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <param name="ponderation"></param>
        /// <param name="tVA"></param>
        /// <param name="price"></param>
        /// <param name="numberInvoice"></param>
        public Invoice(float ponderation, int TVA, int price, int numberInvoice, DateTime dateInvoice)
        {
            Ponderation = ponderation;
            this.TVA = TVA;
            Price = price;
            NumberInvoice = numberInvoice;
            DateInvoice = dateInvoice;
        }

        /// <summary>
        /// Constructor Invoice (facture)
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <param name="ponderation"></param>
        /// <param name="tVA"></param>
        /// <param name="price"></param>
        /// <param name="numberInvoice"></param>
        public Invoice(int InvoiceId, float ponderation, int TVA, int price, int numberInvoice, DateTime dateInvoice)
        {
            this.InvoiceId = InvoiceId;
            Ponderation = ponderation;
            this.TVA = TVA;
            Price = price;
            NumberInvoice = numberInvoice;
            DateInvoice = dateInvoice;
        }

        /// <summary>
        /// Constructor Invoice (facture) with interventions
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <param name="ponderation"></param>
        /// <param name="tVA"></param>
        /// <param name="price"></param>
        /// <param name="numberInvoice"></param>
        /// <param name="interventions"></param>
        public Invoice(int InvoiceId, float ponderation, int TVA, int price, int numberInvoice, DateTime dateInvoice, List<Prestation> prestation) : this(InvoiceId, ponderation, TVA, price, numberInvoice, dateInvoice)
        {
            this.prestation = prestation;
        }
    }
}
