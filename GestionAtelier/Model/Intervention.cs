using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class Intervention
    {
        public int interventionId { get; set; }
        public Vehicle vehicleId { get; set; }
        public InterventionType interventionTypeId { get; set; }
        public Employee Submitter { get; set; }
        public Employee Applicant { get; set; }
        public Status StatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateEstimate { get; set; }
        public bool PieceOrder { get; set; }
        public string PieceCom { get; set; }
        public float TimeEstimate { get; set; }
        public Priority Priority { get; set; }
        public ParmSite SiteId { get; set; }
        public List<Prestation> Prestations { get; set; }
        public string barcodePicture { get; set; }
        public int Barcode { get; set; }
        public bool IsCheckedForGenerateSheet { get; set; }

        /// <summary>
        /// Constructor Intervention
        /// </summary>
        /// <param name="interventionId"></param>
        /// <param name="vehicleId"></param>
        /// <param name="interventionTypeId"></param>
        /// <param name="submitter"></param>
        /// <param name="applicant"></param>
        /// <param name="statusId"></param>
        /// <param name="statusDate"></param>
        /// <param name="dateIn"></param>
        /// <param name="dateOut"></param>
        /// <param name="dateEstimate"></param>
        /// <param name="pieceOrder"></param>
        /// <param name="pieceCom"></param>
        /// <param name="timeEstimate"></param>
        /// <param name="priority"></param>
        /// <param name="siteId"></param>
        public Intervention(int interventionId, Vehicle vehicleId, InterventionType interventionTypeId, Employee submitter, Employee applicant, Status statusId, DateTime statusDate, DateTime dateIn, DateTime dateOut, DateTime dateEstimate, bool pieceOrder, string pieceCom, float timeEstimate, Priority priority, ParmSite siteId)
        {
            this.interventionId = interventionId;
            this.vehicleId = vehicleId;
            this.interventionTypeId = interventionTypeId;
            Submitter = submitter;
            Applicant = applicant;
            StatusId = statusId;
            StatusDate = statusDate;
            DateIn = dateIn;
            DateOut = dateOut;
            DateEstimate = dateEstimate;
            PieceOrder = pieceOrder;
            PieceCom = pieceCom;
            TimeEstimate = timeEstimate;
            Priority = priority;
            SiteId = siteId;
        }

        /// <summary>
        /// Construtor intervention
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="interventionTypeId"></param>
        /// <param name="submitter"></param>
        /// <param name="applicant"></param>
        /// <param name="statusId"></param>
        /// <param name="priority"></param>
        /// <param name="siteId"></param>
        public Intervention(Vehicle vehicleId, InterventionType interventionTypeId, Employee submitter, Employee applicant, Status statusId, Priority priority, ParmSite siteId)
        {
            this.vehicleId = vehicleId;
            this.interventionTypeId = interventionTypeId;
            Submitter = submitter;
            Applicant = applicant;
            StatusId = statusId;
            Priority = priority;
            SiteId = siteId;
        }

        /// <summary>
        /// Constructor with prestation list
        /// </summary>
        /// <param name="interventionId"></param>
        /// <param name="vehicleId"></param>
        /// <param name="interventionTypeId"></param>
        /// <param name="submitter"></param>
        /// <param name="applicant"></param>
        /// <param name="statusId"></param>
        /// <param name="statusDate"></param>
        /// <param name="dateIn"></param>
        /// <param name="dateOut"></param>
        /// <param name="dateEstimate"></param>
        /// <param name="pieceOrder"></param>
        /// <param name="pieceCom"></param>
        /// <param name="timeEstimate"></param>
        /// <param name="priority"></param>
        /// <param name="siteId"></param>
        /// <param name="prestations"></param>
        public Intervention(int interventionId, Vehicle vehicleId, InterventionType interventionTypeId, Employee submitter, Employee applicant, Status statusId, DateTime statusDate, DateTime dateIn, DateTime dateOut, DateTime dateEstimate, bool pieceOrder, string pieceCom, float timeEstimate, Priority priority, ParmSite siteId, List<Prestation> prestations) : this(interventionId, vehicleId, interventionTypeId, submitter, applicant, statusId, statusDate, dateIn, dateOut, dateEstimate, pieceOrder, pieceCom, timeEstimate, priority, siteId)
        {
            Prestations = prestations;
        }

        public Intervention(int interventionId, Vehicle vehicleId, InterventionType interventionTypeId, Employee submitter, Employee applicant, Status statusId, DateTime statusDate, DateTime dateIn, DateTime dateOut, DateTime dateEstimate, bool pieceOrder, string pieceCom, float timeEstimate, Priority priority, ParmSite siteId, List<Prestation> prestations, int Barcode) : this(interventionId, vehicleId, interventionTypeId, submitter, applicant, statusId, statusDate, dateIn, dateOut, dateEstimate, pieceOrder, pieceCom, timeEstimate, priority, siteId, prestations)
        {
            this.Barcode = Barcode;
        }
    }
}
