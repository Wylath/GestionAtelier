using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class InterventionTire
    {
        public int InterventionTireId { get; set; }
        public int TireNumber { get; set; }
        public int ChangeCause { get; set; }
        public int InterventionId { get; set; }
        public Tires NewTireBarcodeId { get; set; }

        /// <summary>
        /// Constructor InterventionTires
        /// </summary>
        /// <param name="interventionTireId"></param>
        /// <param name="tireNumber"></param>
        /// <param name="changeCause"></param>
        /// <param name="interventionId"></param>
        /// <param name="newTireBarcodeId"></param>
        public InterventionTire(int interventionTireId, int tireNumber, int changeCause, int interventionId, Tires newTireBarcodeId)
        {
            InterventionTireId = interventionTireId;
            TireNumber = tireNumber;
            ChangeCause = changeCause;
            InterventionId = interventionId;
            NewTireBarcodeId = newTireBarcodeId;
        }

        /// <summary>
        /// Constructor InterventionTires
        /// </summary>
        /// <param name="tireNumber"></param>
        /// <param name="changeCause"></param>
        /// <param name="interventionId"></param>
        /// <param name="newTireBarcodeId"></param>
        public InterventionTire(int tireNumber, int changeCause, int interventionId, Tires newTireBarcodeId)
        {
            TireNumber = tireNumber;
            ChangeCause = changeCause;
            InterventionId = interventionId;
            NewTireBarcodeId = newTireBarcodeId;
        }
    }
}
